using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ClientSVH.DocRecordDataAccess.Entities
{
    public class DocRecordEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("doc_id")]
        public string DocId { get; set; }
        [BsonElement("doc_body")]
        public string DocText { get; set; } = null!;

    }
}
