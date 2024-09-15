using ClientSVH.DocsBodyCore.Models;

namespace ClientSVH.DocsBodyCore.Abstraction
{
    public interface IDocRecordServices
    {
        Task Add(DocRecord docRecord);
        Task Delete(Guid Docid);
        Task<DocRecord?> GetId(Guid docId);
        Task Update(Guid Docid, DocRecord docRecord);
    }
}