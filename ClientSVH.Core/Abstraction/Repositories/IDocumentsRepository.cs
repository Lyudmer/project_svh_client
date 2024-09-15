using ClientSVH.Core.Models;
using ClientSVH.DocsBodyCore.Models;

namespace ClientSVH.Core.Abstraction.Repositories
{
    public interface IDocumentsRepository
    {
        Task<int> Add(int PkgId, Document Doc, DocRecord docRecord);
        Task Delete(int Id);
        Task<List<Document>> GetByFilter(int Pid);
        Task<Document> GetById(int Id);
        Task<List<Document>> GetByPage(int Page, int Page_Size);
        Task Update(int Id);
    }
}