using Microsoft.EntityFrameworkCore;
using ClientSVH.Core.Models;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.DataAccess.Entities;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;

namespace ClientSVH.DataAccess.Repositories
{
    public class DocumentsRepository(ClientSVHDbContext dbContext) : IDocumentsRepository
    {
        private readonly ClientSVHDbContext _dbContext = dbContext;
        public async Task<Document> Add(Document Doc)
        {
            await _dbContext.AddAsync(Doc);
            await _dbContext.SaveChangesAsync();
            
            return Doc;
        }
        public async Task<Document> GetById(int id)
        {
            var docEntity = await _dbContext.Document
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id) ?? throw new Exception();

            return MappedObj(docEntity);

        }
        public async Task<List<Document>> GetByFilter(int pid)
        {
            var query = _dbContext.Document.AsNoTracking();

            if (pid > 0) { query = query.Where(p => p.Pid == pid); }

            var docs = await query.ToListAsync();
            return MappedObj(docs);

        }
        public async Task<List<Document>> GetByPage(int page, int page_size)
        {
            var query = _dbContext.Document
                .AsNoTracking()
                .Skip((page - 1) * page_size)
                .Take(page_size);

            var docs = await query.ToListAsync();
            return MappedObj(docs);
        }

        public async Task Update(int Id)
        {
            await _dbContext.Document
                .Where(p => p.Id == Id)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.ModifyDate, DateTime.Now));
        }
        public async Task Delete(int Id)
        {
             await _dbContext.Document
                    .Where(u => u.Id == Id)
                    .ExecuteDeleteAsync();
        }
        
        public async Task<int> GetLastDocId()
        {
            return await _dbContext.Document.MaxAsync(p => p.Id);
        }
        private static Document MappedObj(DocumentEntity docEntity)
        {
            return Document.Create(docEntity.Id, docEntity.DocId, docEntity.Number, docEntity.DocDate, docEntity.ModeCode, docEntity.DocType, 
                                   docEntity.SizeDoc,docEntity.Idmd5, docEntity.IdSha256, docEntity.Pid, docEntity.CreateDate, docEntity.ModifyDate);
        }
        private static List<Document> MappedObj(List<DocumentEntity> docEntity)
        {
            List<Document> result = [];
            foreach (DocumentEntity doc in docEntity)
            {
                result.Add(MappedObj(doc));
            }
            return result;
        }
    }
}
