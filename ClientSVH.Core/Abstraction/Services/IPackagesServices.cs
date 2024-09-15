using ClientSVH.Core.Models;

namespace ClientSVH.Core.Abstraction.Services
{
    public interface IPackagesServices
    {
    
        Task<int> LoadFile(Guid UserId, string FileName);
        Task<Package> Add(Package Pkg);
        Task<Package> GetPkgId(int Pid);
        Task<int> GetLastPkgId();
        Task<int> SendToServer(int Pid);

    }
}