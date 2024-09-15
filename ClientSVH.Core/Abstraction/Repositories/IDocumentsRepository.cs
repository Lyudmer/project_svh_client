using ClientSVH.Core.Models;
using ClientSVH.DocsBodyCore.Models;

namespace ClientSVH.Core.Abstraction.Repositories
{
    public interface IDocumentsRepository
    {
        Task<Document> Add(Document Doc, DocRecord docRecord);
        Task Delete(int Id);
        Task<List<Document>> GetByFilter(int Pid);
        Task<Document> GetById(int Id);
        Task<List<Document>> GetByPage(int Page, int Page_Size);
        Task Update(int Id);
        Task DeleteById(int Id);
        Task<List<Document>> GetAll();
        Task DeleteAll();
        Task<int> GetLastDocId();
    }
}