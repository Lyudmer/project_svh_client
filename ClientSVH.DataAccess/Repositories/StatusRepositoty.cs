using ClientSVH.Core.Abstaction.Repositories;
using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientSVH.DataAccess.Repositories
{
    public class StatusRepositoty : IStatusRepositoty
    {
        private readonly ClientSVHDbContext _dbContext;

        public StatusRepositoty(ClientSVHDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(int Id, string StatusName, bool RunWf, bool MkRes)
        {
            var statusEntity = new StatusEntity
            {
                Id = Id,
                StatusName = StatusName,
                RunWf = RunWf,
                MkRes = MkRes
            };
            await _dbContext.AddAsync(statusEntity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(int Id, string StatusName, bool RunWf, bool MkRes)
        {
            await _dbContext.Status
                .Where(u => u.Id == Id)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.StatusName, StatusName)
                                          .SetProperty(u => u.RunWf, RunWf)
                                          .SetProperty(u => u.MkRes, MkRes));
        }
        public async Task Delete(int Id)
        {
            await _dbContext.Status
                .Where(u => u.Id == Id)
                .ExecuteDeleteAsync();
        }
    }
}
