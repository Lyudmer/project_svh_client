using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClientSVH.DocsRecordCore.Models
{
    public class DocRecord
    {

        [BsonId]
        public ObjectId Id { get; set; }
        public string DocId { get; set; }
        public string DocText { get; set; } = null!;
        public static DocRecord Create( string docId, string docText)
        {
            var docrecord = new DocRecord() {DocId = docId, DocText = docText};
            return docrecord;
        }

    }



}
