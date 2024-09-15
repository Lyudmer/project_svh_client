using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.Application.Interfaces.Auth;


namespace ClientSVH.Application.Services
{
    public class PackagesServices(ILoadFromFile loadFromFile,
        IPackagesRepository pkgRepository,
        ISendToServer sendToServer
        ) : IPackagesServices
    {
        
        private readonly ILoadFromFile _loadFromFile = loadFromFile;
        private readonly IPackagesRepository _pkgRepository = pkgRepository;
        private readonly ISendToServer _sendToServer = sendToServer;    
        public Task<int> LoadFile(Guid UserId, string FileName)
        {
            return _loadFromFile.LoadFile(UserId, FileName);
        }
        public async Task<Package> Add(Package Pkg)
        {
            return await _pkgRepository.Add(Pkg);
        }
        public async Task<Package> GetPkgId(int Pid)
        {
            return await _pkgRepository.GetById(Pid);
        }
        public async Task<int> GetLastPkgId()
        {
            return await _pkgRepository.GetLastPkgId();
        }
        public async Task<int> SendToServer(int Pid)
        {
            return await _sendToServer.SendToServer(Pid);
        }
    }
}
