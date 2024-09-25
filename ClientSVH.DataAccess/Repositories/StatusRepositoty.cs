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

        public async Task<int> Add(int Id, string StatusName, bool RunWf, bool MkRes, bool SendMess)
        {
            var statusEntity = new StatusEntity
            {
                Id = Id,
                StatusName = StatusName,
                RunWf = RunWf,
                MkRes = MkRes,
                SendMess=SendMess
            };
            await _dbContext.AddAsync(statusEntity);
            await _dbContext.SaveChangesAsync();
            return statusEntity.Id;
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
        public async Task<Status> GetById(int Id)
        {
            var stEntity = await _dbContext.Status
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == Id) ?? throw new Exception();

            return _mapper.Map<Status>(stEntity);

        }
    }
}
