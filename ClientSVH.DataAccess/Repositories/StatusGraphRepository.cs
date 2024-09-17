using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using ClientSVH.Core.Abstraction.Repositories;


namespace ClientSVH.DataAccess.Repositories
{
    public class StatusGraphRepository(ClientSVHDbContext dbContext) : IStatusGraphRepository
    {
        private readonly ClientSVHDbContext _dbContext = dbContext;

        public async Task Add(int oldst, int newst, string maskbit)
        {
            var statusGraphEntity = new StatusGraphEntity
            {
                OldSt = oldst,
                NewSt = newst,

            };
            await _dbContext.AddAsync(statusGraphEntity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateOld(int oldst, int newst, string maskbit)
        {

            await _dbContext.StatusGraph
                .Where(u => u.OldSt == oldst)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.NewSt, newst));
        }
        public async Task UpdateNew(int oldst, int newst, string maskbit)
        {

            await _dbContext.StatusGraph
                .Where(u => u.NewSt == newst)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.OldSt, oldst));
        }
        public async Task Delete(int oldst, int newst)
        {

            await _dbContext.StatusGraph
                .Where(u => u.OldSt == oldst)
                .Where(u => u.NewSt == newst)
                .ExecuteDeleteAsync();
        }
    }
}
