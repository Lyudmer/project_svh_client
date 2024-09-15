using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using AutoMapper;


namespace ClientSVH.DataAccess.Repositories
{
    public class DocumentRepository : IDocumentsRepository
    {
        private readonly ClientSVHDbContext _dbContext;
        private readonly IMapper _mapper;
        public DocumentRepository(ClientSVHDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<int> Add(int PkgId, Document Doc)
        {
            var docEntity = new DocumentEntity
            {
                Pid = PkgId,
                Id = Doc.Id,
                DocId = Doc.DocId,
                Number = Doc.Number,
                DocDate = Doc.DocDate,
                ModeCode = Doc.ModeCode,
                SizeDoc = Doc.SizeDoc,
                Idmd5 = Doc.Idmd5,
                IdSha256 = Doc.IdSha256,
                CreateDate = Doc.CreateDate,
                ModifyDate = Doc.ModifyDate
            };
            await _dbContext.AddAsync(docEntity);
            await _dbContext.SaveChangesAsync();
            return docEntity.Id;
        }
        public async Task<Document> GetById(int id)
        {
            var docEntity = await _dbContext.Document
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id) ?? throw new Exception();

            return _mapper.Map<Document>(docEntity);

        }
        public async Task<List<Document>> GetByFilter(int pid)
        {
            var query = _dbContext.Document.AsNoTracking();

            if (pid > 0) { query = query.Where(p => p.Pid == pid); }

            var docs = await query
             .Select(d => Document.Create(d.Id, d.DocId, d.Number, d.DocDate, d.ModeCode, d.SizeDoc,
                                           d.Idmd5, d.IdSha256, pid, d.CreateDate, d.ModifyDate))
             .ToListAsync();
            return docs;
        }
        public async Task<List<Document>> GetByPage(int page, int page_size)
        {
            var query = _dbContext.Document
                .AsNoTracking()
                .Skip((page - 1) * page_size)
                .Take(page_size);

            var docs = await query
            .Select(d => Document.Create(d.Id, d.DocId, d.Number, d.DocDate, d.ModeCode, d.SizeDoc,
                                          d.Idmd5, d.IdSha256, d.Pid, d.CreateDate, d.ModifyDate))
            .ToListAsync();
            return docs;
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
    }
}
