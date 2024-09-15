using MongoDB.Bson.Serialization.Attributes;

namespace ClientSVH.Contracts
{
    record class DocRecordResponse
    (
        Guid Id,
        Guid DocId,
        string DocText,
        DateTime CreateDate,
        DateTime ModifyDate
     );
}
