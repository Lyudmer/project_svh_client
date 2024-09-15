using ClientSVH.Core.Models;

namespace ClientSVH.Core.Abstaction.Services
{
    public interface IPackagesServices
    {
    
        Task<int> LoadFile(Guid UserId, string FileName);
        Task<int> Add(Guid UserId, Package Pkg);
        Task<int> GetLastPkgId();


    }
}