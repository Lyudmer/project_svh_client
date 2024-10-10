using ClientSVH.Core.Models;
using ClientSVH.DocsRecordCore.Models;

namespace ClientSVH.Core.Abstraction.Services
{
    public interface IDocumentsServices
    {
        Task<Document> GetDocId(int Id);
        Task<DocRecord> GetDocRecord(int Id);
        Task<bool> DeleteDoc(int Id);
    }
}