using ClientSVH.Core.Models;
using ClientSVH.DocsBodyCore.Models;

namespace ClientSVH.Core.Abstraction.Services
{
    public interface IDocumentsServices
    {
        Task<Document> Add(Document Doc, DocRecord docRecord);
        Task Delete(int Id);
        Task<List<Document>> GetByFilter(int Pid);
        Task<Document> GetById(int Id);
        Task<List<Document>> GetByPage(int Page, int Page_Size);
        Task Update(int Id);
        Task<int> GetLastDocId();
        Task<DocRecord> GetDocWithRecord(Guid DocId);
    }
}