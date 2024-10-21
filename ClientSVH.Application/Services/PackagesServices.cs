using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Application.Interfaces;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using ClientSVH.Application.CollectingListToXml.Hendlers;



namespace ClientSVH.Application.Services
{
    public class PackagesServices(ILoadFromFile loadFromFile,
        ISendToServer sendToServer, IReceivFromServer receivFromServer, IHttpContextAccessor httpContextAccessor,
        IPackagesRepository pkgRepository, IDocumentsRepository documentsRepository,
        IHistoryPkgRepository hPkgRepository, IDocumentsServices documentServices
        ) : IPackagesServices
    {
        
        private readonly ILoadFromFile _loadFromFile = loadFromFile;
        private readonly ISendToServer _sendToServer = sendToServer;    
        private readonly IReceivFromServer _receivFromServer = receivFromServer;
        private readonly IHistoryPkgRepository _hPkgRepository = hPkgRepository;
        private readonly IPackagesRepository _pkgRepository = pkgRepository;
        private readonly IDocumentsRepository _documentsRepository = documentsRepository;
        private readonly IDocumentsServices _documentServices= documentServices;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        
        public async Task<HistoryPkg> HistoriPkgByPid(int Pid)
        {
            return await _hPkgRepository.GetById(Pid);
        }
        public Task<string> LoadFile(Guid UserId,string  FileName)
        {
            return _loadFromFile.LoadFileXml(UserId, FileName);
        }
        public async Task<int> SendToServer(int Pid)
        {
            return await _sendToServer.SendPaskageToServer(Pid);
        }
        public async Task<bool> SendDelPkgToServer(int Pid)
        {
            return await _sendToServer.SendDelPkgToServer(Pid);
        }
        public async Task<int> LoadMessage()
        {
            return await _receivFromServer.LoadMessage();
        }
        public async Task<int> PkgFLK(int Pid)
        {
            return await _sendToServer.PkgFLK(Pid);
        }
        public async Task<List<Package>> GetAll()
        {
            return await  _pkgRepository.GetAll();
        }

        public async Task<Package> GetPkgId(int Pid)
        {
            return await _pkgRepository.GetById(Pid);
        }
        public async Task<List<Document>> GetDocsPkg(int Pid)
        {
            return await _documentsRepository.GetByFilter(Pid);
        }
       
        public async Task<string> DeletePkg(int Pid)
        {
            if (Pid != 0)
            {
                var Docs= await _documentsRepository.GetByFilter(Pid);
                int cDocs=Docs.ToList().Count;
                foreach (var Doc in Docs) 
                {
                    if (await _documentServices.DeleteDoc(Doc.Id)) cDocs--;

                }
                if (cDocs == 0)
                    await _pkgRepository.Delete(Pid);
                var resDel = await _pkgRepository.GetById(Pid);
                if(resDel is null)
                    return "Пакет удален";
                else 
                {
                    return "Ошибка удаления";
                }
            }
            return $"Ошибка удаления, пакет с номером {Pid} не существует";
        }
    }
}
