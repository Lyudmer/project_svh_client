using ClientSVH.DocsBodyCore.Models;

using AutoMapper;
using ClientSVH.DocsBodyDataAccess;
using ClientSVH.DocsBodyDataAccess.Entities;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ClientSVH.DocsBodyCore.Abstraction;
namespace ClientSVH.DocsBodyCore.Repositories
{
    public class DocRecordRepository : IDocRecordRepository
    {
        private readonly DocsBodyDBConnectionSettings _context;

        private readonly IMongoCollection<DocRecord> _docBodyCollection;
        public DocRecordRepository(DocsBodyDBConnectionSettings context,
            IOptions<DocsBodyDBConnectionSettings> DocBodyDBSettings)
        {
            _context = context;


            var mongoClient = new MongoClient(
                DocBodyDBSettings.Value.MongoDBContext);

            var mongoDatabase = mongoClient.GetDatabase(
               DocBodyDBSettings.Value.DatabaseName);

            _docBodyCollection = mongoDatabase.GetCollection<DocRecord>(
                DocBodyDBSettings.Value.DBCollectionName);
        }

        public async Task<DocRecord?> GetByDocId(Guid docId) =>
        await _docBodyCollection.Find(x => x.DocId == docId).FirstOrDefaultAsync();

        public async Task Add(DocRecord docRecord) =>
            await _docBodyCollection.InsertOneAsync(docRecord);

        public async Task Update(Guid Docid, DocRecord docRecord) =>
           await _docBodyCollection.ReplaceOneAsync(x => x.DocId == Docid, docRecord);

        public async Task DeleteId(Guid Docid) =>
            await _docBodyCollection.DeleteOneAsync(x => x.DocId == Docid);



    }
}
