using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSVH.DocsBodyDataAccess.Entities
{
    [NotMapped]
    public class DocRecordEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        
        [BsonElement("doc_id")]
        public Guid DocId { get; set; }
        [BsonElement("doc_body")]
        public string DocText { get; set; } = null!;

        [BsonElement("create_date")]
        public DateTime CreateDate { get; set; }= DateTime.Now;
        [BsonElement("modify_date")]
        public DateTime ModifyDate { get; set; } = DateTime.Now;


    }
}
