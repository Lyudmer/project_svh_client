using ClientSVH.Core.Models;
using ClientSVH.DocsBodyCore.Models;

namespace ClientSVH.DataAccess.Repositories
{
    public interface IDocumentsRepository
    {
        Task<Document> Add(Document Doc, DocRecord docRecord);
        Task Delete(int Id);
        Task<List<Document>> GetByFilter(int pid);
        Task<Document> GetById(int id);
        Task<List<Document>> GetByPage(int page, int page_size);
        Task<DocRecord> GetDocWithRecord(Guid DocId);
        Task<int> GetLastDocId();
        Task Update(int Id);
    }
}