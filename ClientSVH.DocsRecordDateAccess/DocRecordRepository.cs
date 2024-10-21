
using ClientSVH.DocsRecordCore.Abstraction;
using ClientSVH.DocsRecordCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using MongoDB.Driver;
namespace ClientSVH.DocsRecordDataAccess
{
    public class DocRecordRepository : IDocRecordRepository
    {

        private readonly IDocRecordContext _docRecordCollection;
        public DocRecordRepository(IOptions<Settings> settings)
        { 
             _docRecordCollection = new DocRecordContext(settings);

        }
        public async Task<DocRecord> GetByDocId(Guid docId)
        {
           // var filter = Builders<DocRecord>.Filter.Eq("DocId", docId.ToString());

            try
            {
               var resRec= await _docRecordCollection.DocRecords
                                .Find(x => x.DocId == docId.ToString())
                                .FirstOrDefaultAsync();
                if (resRec == null) return null;
                else return resRec;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<string> AddRecord(DocRecord item)
        {
            try
            {
                var options = new InsertOneOptions { BypassDocumentValidation = true };
                await _docRecordCollection.DocRecords.InsertOneAsync(item,options);

                return item.DocId;
            }
            catch (Exception)
            {
               return string.Empty;
            }
        }
        public async Task<long> UpdateRecord(Guid DocId, DocRecord docRecord)
        {
            var filter = Builders<DocRecord>.Filter.Eq(s => s.DocId, DocId.ToString());
            var update = Builders<DocRecord>.Update.Set(s => s.DocText, docRecord.DocText);
            try
            {
                var resUpdate = await _docRecordCollection.DocRecords.UpdateOneAsync(filter, update);
                if (resUpdate != null) return resUpdate.ModifiedCount;
                else return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<long> DeleteId(string Docid)
        {
            try
            {
                var resDel = await _docRecordCollection.DocRecords.DeleteOneAsync(x => x.DocId == Docid.ToString());
                if (resDel != null) return resDel.DeletedCount;
                else return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }


    }
}
