﻿

using AutoMapper;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientSVH.DataAccess.Repositories
{
    public class HistoryPkgRepository(ClientSVHDbContext dbContext, IMapper mapper) : IHistoryPkgRepository
    {
        private readonly ClientSVHDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        public async Task<HistoryPkg> Add(HistoryPkg HpPkg)
        {
            await _dbContext.AddAsync(HpPkg);
            await _dbContext.SaveChangesAsync();
            var nRes = await _dbContext.SaveChangesAsync();
            if (nRes > 0) return HpPkg;
            else return null;
           
        }
        public async Task<HistoryPkg> GetById(int Pid)
        {
            var hpPkgEntity = await _dbContext.HistoryPkg
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Pid == Pid) ?? throw new Exception();
            return _mapper.Map<HistoryPkg>(hpPkgEntity);
        }
       
        
    }
}
