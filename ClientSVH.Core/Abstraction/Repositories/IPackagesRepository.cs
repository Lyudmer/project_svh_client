using ClientSVH.Core.Models;

namespace ClientSVH.Core.Abstraction.Repositories
{
    public interface IPackagesRepository
    {
        Task<Package> Add(Package Pkg);
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