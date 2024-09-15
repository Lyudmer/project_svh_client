
using ClientSVH.Core.Abstaction.Services;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;


namespace ClientSVH.Application.Services
{
    public class DocumentsServices : IDocumentsServices
    {
        private readonly IDocumentsRepository _docRepository;
        public DocumentsServices(IDocumentsRepository docRepository)
        {
            _docRepository = docRepository;
        }

        public Task<int> Add(int PkgId, Document Doc)
        {
            return _docRepository.Add(PkgId, Doc);
        }

        public Task Delete(int Id)
        {
            return _docRepository.Delete(Id);
        }

        public Task<List<Document>> GetByFilter(int Pid)
        {
            return _docRepository.GetByFilter(Pid);
        }

        public Task<Document> GetById(int Id)
        {
            return _docRepository.GetById(Id);
        }

        public Task<List<Document>> GetByPage(int Page, int Page_Size)
        {
            return _docRepository.GetByPage(Page, Page_Size);
        }

        public Task Update(int Id)
        {
            return _docRepository.GetById(Id);
        }
    }
}
