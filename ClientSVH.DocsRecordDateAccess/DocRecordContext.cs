
using ClientSVH.DocsRecordCore.Abstraction;
using ClientSVH.DocsRecordCore.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ClientSVH.DocsRecordDataAccess
{
    public class DocRecordContext : IDocRecordContext
    {
        private readonly IMongoDatabase _database = null;
        public DocRecordContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
               

            }
        }
        public IMongoCollection<DocRecord> DocRecords
        {
            get
            {
                return GeDocRecord();
            }

        }

        private  IMongoCollection<DocRecord> GeDocRecord()
        {
            var collection = _database.GetCollection<DocRecord>("DocRecord");
            if (collection != null)
                return collection;
            else
            {
                _database.CreateCollection("DocRecord");
                return _database.GetCollection<DocRecord>("DocRecord");
            }
        }
    }
}
