using ClientSVH.DocsRecordCore.Models;

namespace ClientSVH.DocsRecordCore.Abstraction
{
    public interface IDocRecordRepository
    {
        Task<Guid> Add(DocRecord docRecord);
        Task DeleteId(Guid Docid);
        Task<DocRecord?> GetByDocId(Guid docId);
        Task Update(Guid Docid, DocRecord docRecord);
    }
}