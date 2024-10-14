
using AutoMapper;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientSVH.DataAccess.Repositories
{
    public class StatusRepositoty(ClientSVHDbContext dbContext, IMapper mapper) : IStatusRepositoty
    {
        private readonly ClientSVHDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        public async Task<int> Add(Status status)
        {
            await _dbContext.AddAsync(status);
            var nRes = await _dbContext.SaveChangesAsync();
            if (nRes > 0) return status.Id;
            else return -1;
        }
        public async Task Update(Status status)
        {
            await _dbContext.Status
                .Where(u => u.Id == status.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.StatusName, status.StatusName)
                                          .SetProperty(u => u.RunWf, status.RunWf)
                                          .SetProperty(u => u.MkRes, status.MkRes));
        }
        public async Task Delete(int Id)
        {
            await _dbContext.Status
                .Where(u => u.Id == Id)
                .ExecuteDeleteAsync();

        }
        public async Task<Status> GetById(int Id)
        {
            var stEntity = await _dbContext.Status
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == Id) ?? throw new Exception();

            return _mapper.Map<Status>(stEntity);
            

        }
    }
}
