using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.Application.Interfaces.Auth;



namespace ClientSVH.Application.Services
{
    public class PackagesServices(ILoadFromFile loadFromFile,
        ISendToServer sendToServer
        ) : IPackagesServices
    {
        
        private readonly ILoadFromFile _loadFromFile = loadFromFile;
        private readonly ISendToServer _sendToServer = sendToServer;    
        public Task<int> LoadFile(Guid UserId, string FileName)
        {
            return _loadFromFile.LoadFile(UserId, FileName);
        }
    
        public async Task<int> SendToServer(int Pid)
        {
            return await _sendToServer.SendPaskageToServer(Pid);
        }
    }
}
