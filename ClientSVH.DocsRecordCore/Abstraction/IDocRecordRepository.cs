using ClientSVH.DocsRecordCore.Models;

namespace ClientSVH.DocsRecordCore.Abstraction
{
    public interface IDocRecordRepository
    {
        Task<Guid> AddRecord(DocRecord item);
        Task<long> DeleteId(Guid Docid);
        Task<DocRecord> GetByDocId(Guid docId);
        Task<long> UpdateRecord(Guid DocId, DocRecord docRecord);
    }
}