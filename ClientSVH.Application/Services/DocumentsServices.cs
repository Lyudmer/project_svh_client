using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Models;
using ClientSVH.DocsBodyCore.Models;


namespace ClientSVH.Application.Services
{
    public class DocumentsServices(IDocumentsRepository docRepository) : IDocumentsServices
    {
        private readonly IDocumentsRepository _docRepository = docRepository;

        public Task<Document> Add(Document Doc, DocRecord docRecord)
        {
            return _docRepository.Add(Doc, docRecord);
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
        public Task<int> GetLastDocId()
        {
            return _docRepository.GetLastDocId();
        }
        public Task<DocRecord> GetDocWithRecord(Guid DocId)
        {
            return _docRepository.GetDocWithRecord(DocId);
        }
    }
}
