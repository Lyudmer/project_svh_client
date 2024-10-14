

using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientSVH.DataAccess.Repositories
{
    public class HistoryPkgRepository(ClientSVHDbContext dbContext) : IHistoryPkgRepository
    {
        private readonly ClientSVHDbContext _dbContext = dbContext;
        
        public async Task<HistoryPkg> Add(HistoryPkg HpPkg)
        {
            await _dbContext.AddAsync(HpPkg);
            await _dbContext.SaveChangesAsync();
            return HpPkg;
        }
        public async Task<HistoryPkg> GetById(int Pid)
        {
            var hpPkgEntity = await _dbContext.HistoryPkg
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Pid == Pid) ?? throw new Exception();

            return MappedObj(hpPkgEntity);

        }
        private static HistoryPkg MappedObj(HistoryPkgEntity hPkgEntity)
        {
            return HistoryPkg.Create( hPkgEntity.Pid, hPkgEntity.Oldst, hPkgEntity.Newst, hPkgEntity.Comment, hPkgEntity.CreateDate);
        }
        
    }
}
