﻿using ClientSVH.Core.Models;

namespace ClientSVH.Core.Abstaction.Repositories
{
    public interface IPackagesRepository
    {
        Task<int> Add(Guid UserId, Package Pkg);
        Task Delete(int Pid);
        Task<List<Package>> GetAll();
        Task<List<Package>> GetPkgUser(Guid UserId);
        Task<List<Package>> GetUserStatus(Guid UserId, int Status);
        Task<Package> GetById(int id);
        Task<List<Package>> GetByPage(int Page, int Page_Size);
        Task<Package> GetPkgWithDoc(int Pid);
        Task UpdateStatus(int Pid, int Status);
        Task<int> GetLastPkgId();
    }
}