using ClientSVH.DocsBodyCore.Models;

namespace ClientSVH.DocsBodyCore.Abstraction
{
    public interface IDocRecordRepository
    {
        Task<Guid> Add(DocRecord docRecord);
        Task DeleteId(Guid Docid);
        Task<DocRecord?> GetByDocId(Guid docId);
        Task Update(Guid Docid, DocRecord docRecord);
    }
}