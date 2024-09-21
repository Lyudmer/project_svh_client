using ClientSVH.Core.Models;

namespace ClientSVH.Core.Abstraction.Services
{
    public interface IPackagesServices
    {
    
        Task<int> LoadFile(Guid UserId, string FileName);
      
        Task<int> SendToServer(int Pid);

    }
}