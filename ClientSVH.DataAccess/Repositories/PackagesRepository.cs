using AutoMapper;
using ClientSVH.Core.Abstaction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace ClientSVH.DataAccess.Repositories
{
    public class PackagesRepository : IPackagesRepository
    {
        private readonly ClientSVHDbContext _dbContext;
        private readonly IMapper _mapper;
       
        public PackagesRepository(ClientSVHDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Add(Guid UserId, Package Pkg)
        {
            var pkgEntity = new PackageEntity
            {
                Id = Pkg.Pid,
                UserId = UserId,
                StatusId = Pkg.StatusId,
                CreateDate = Pkg.CreateDate,
                ModifyDate = Pkg.ModifyDate
            };
            await _dbContext.AddAsync(pkgEntity);
            await _dbContext.SaveChangesAsync();
            return pkgEntity.Id;
        }
        public async Task<Package> GetById(int Pid)
        {
            var pkgEntity = await _dbContext.Packages
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == Pid) ?? throw new Exception();

            return _mapper.Map<Package>(pkgEntity);

        }
        public async Task<List<Package>> GetAll()
        {
            var query = _dbContext.Packages
                .AsNoTracking()
                .OrderBy(p => p.UserId)
                .ThenBy(p => p.Id);
            var pkgList = await query
                .Select(p => Package.Create(p.Id, p.UserId, p.StatusId, p.UUID, p.CreateDate, p.ModifyDate))
                .ToListAsync();
            return pkgList;
        }
        public async Task<List<Package>> GetPkgUser(Guid UserId)
        {
            var query = _dbContext.Packages
                .AsNoTracking()
                .OrderBy(p => p.Id);
            var pkgList = await query
                .Select(p => Package.Create(p.Id, UserId, p.StatusId, p.UUID, p.CreateDate, p.ModifyDate))
                .ToListAsync();
            return pkgList;
        }
        public async Task<Package> GetPkgWithDoc(int Pid)
        {
            var pkgEntity = await _dbContext.Packages
                .AsNoTracking()
                .Include(p => p.Documents)
                .FirstOrDefaultAsync(p => p.Id == Pid)
                ?? throw new Exception();

            return _mapper.Map<Package>(pkgEntity);

        }
        public async Task<List<Package>> GetUserStatus(Guid UserId, int Status)
        {
            var query = _dbContext.Packages.AsNoTracking();
            var user = UserId.ToString();
            if (user is not null)
            { query = query.Where(p => p.UserId == UserId); }
            if (Status > -1)
            { query = query.Where(p => p.StatusId == Status); }
            var pkgList = await query
               .Select(p => Package.Create(p.Id, UserId, Status, p.UUID, p.CreateDate, p.ModifyDate))
               .ToListAsync();
            return pkgList;
        }
        public async Task<List<Package>> GetByPage(int Page, int Page_Size)
        {
            var query = _dbContext.Packages
                .AsNoTracking()
                .Skip((Page - 1) * Page_Size)
                .Take(Page_Size);
            var pkgList = await query
                .Select(p => Package.Create(p.Id, p.UserId, p.StatusId, p.UUID, p.CreateDate, p.ModifyDate))
                .ToListAsync();
            return pkgList;

        }
        public async Task UpdateStatus(int Pid, int Status)
        {
            await _dbContext.Packages
                .Where(p => p.Id == Pid)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.StatusId, Status)
                                          .SetProperty(p => p.ModifyDate, DateTime.Now));
        }
        public async Task Delete(int Pid)
        {
            await _dbContext.Packages
                .Where(u => u.Id == Pid)
                .ExecuteDeleteAsync();
        }
        public async Task<int> GetLastPkgId()
        {
            return await _dbContext.Packages.MaxAsync(p => p.Id);
        }
     
        
    }
}
