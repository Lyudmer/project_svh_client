
using AutoMapper;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace ClientSVH.DataAccess.Repositories
{
    public class PackagesRepository(ClientSVHDbContext dbContext, IMapper mapper) : IPackagesRepository
    {
        private readonly ClientSVHDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        public async Task<Package> Add(Package Pkg)
        {
            var PkgEntity = _mapper.Map<PackageEntity>(Pkg);
            var resPkg= await _dbContext.AddAsync(PkgEntity);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<Package>(resPkg.Entity);
        }
        public async Task<Package> GetByUUId(Guid uuid)
        {
            var pkgEntity = await _dbContext.Packages
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UUID == uuid) ?? throw new Exception();
            return _mapper.Map<Package>(pkgEntity);

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
            var pkgList = await query.ToListAsync();
            return _mapper.Map<List<Package>>(pkgList);
        }
        public async Task<List<Package>> GetPkgUser(Guid UserId)
        {
            var query = _dbContext.Packages
                .AsNoTracking()
                .OrderBy(p => p.Id);
            var pkgList = await query.ToListAsync();
            return _mapper.Map<List<Package>>(pkgList);
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
            var pkgList = await query.ToListAsync();
            return _mapper.Map<List<Package>>(pkgList);
        }
        public async Task<List<Package>> GetByPage(int Page, int Page_Size)
        {
            var query = _dbContext.Packages
                .AsNoTracking()
                .Skip((Page - 1) * Page_Size)
                .Take(Page_Size);
            var pkgList = await query.ToListAsync();
            return _mapper.Map<List<Package>>(pkgList);

        }
        public async Task UpdateStatus(int Pid, int Status)
        {
            await _dbContext.Packages
                .Where(p => p.Id == Pid)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.StatusId, Status)
                                          .SetProperty(p => p.ModifyDate, DateTime.UtcNow));
        }
        public async Task Delete(int Pid)
        {
            await _dbContext.Packages
                .Where(u => u.Id == Pid)
                .ExecuteDeleteAsync();
        }
        public async Task<int> GetLastPkgId()
        {
            int cPkg = 0;
            try
            {
                cPkg = await _dbContext.Packages.CountAsync();
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
            return cPkg;
        }

    }
}
