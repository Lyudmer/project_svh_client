using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Application.Interfaces;



namespace ClientSVH.Application.Services
{
    public class PackagesServices(ILoadFromFile loadFromFile,
        ISendToServer sendToServer, IReceivFromServer receivFromServer,
        IHistoryPkgRepository hPkgRepository) : IPackagesServices
    {
        
        private readonly ILoadFromFile _loadFromFile = loadFromFile;
        private readonly ISendToServer _sendToServer = sendToServer;    
        private readonly IReceivFromServer _receivFromServer = receivFromServer;
        private readonly IHistoryPkgRepository _hPkgRepository = hPkgRepository;
        public async Task<HistoryPkg> HistoriPkgByPid(int Pid)
        {
            return await _hPkgRepository.GetById(Pid);
        }
        public Task<int> LoadFile(Guid UserId, string FileName)
        {
            return _loadFromFile.LoadFile(UserId, FileName);
        }
    
        public async Task<int> SendToServer(int Pid)
        {
            return await _sendToServer.SendPaskageToServer(Pid);
        }
        public async Task<int> LoadMessage()
        {
            return await _receivFromServer.LoadMessage();
        }
    }
}
