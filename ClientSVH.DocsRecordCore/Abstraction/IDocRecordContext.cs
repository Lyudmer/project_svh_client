using ClientSVH.DocsRecordCore.Models;
using MongoDB.Driver;

namespace ClientSVH.DocsRecordCore.Abstraction
{
    public interface IDocRecordContext
    {
        IMongoCollection<DocRecord> DocRecords { get; }
    }
}