﻿using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using AutoMapper;
using ClientSVH.DocsBodyCore.Models;
using ClientSVH.DocsBodyCore.Repositories;
using ClientSVH.DocsBodyCore.Abstraction;
using System.Diagnostics;


namespace ClientSVH.DataAccess.Repositories
{
    public class DocumentRepository : IDocumentsRepository
    {
        private readonly ClientSVHDbContext _dbContext;
        private readonly IDocRecordRepository _docRecordRepository;
        private readonly IMapper _mapper;

        public DocumentRepository(ClientSVHDbContext dbContext, IMapper mapper, DocRecordRepository docRecordRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _docRecordRepository = docRecordRepository;
        }
        public async Task<int> Add(int PkgId, Document Doc, DocRecord docRecord)
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
            await _docRecordRepository.Add(docRecord);
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

            var docs = await query.ToListAsync();
            return _mapper.Map<List<Document>>(docs);
            
        }
        public async Task<List<Document>> GetByPage(int page, int page_size)
        {
            var query = _dbContext.Document
                .AsNoTracking()
                .Skip((page - 1) * page_size)
                .Take(page_size);

            var docs = await query.ToListAsync();
            return _mapper.Map<List<Document>>(docs);
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
        public async Task<DocRecord> GetDocWithRecord(Guid DocId)
        {
            var docEntity = await _dbContext.Document
                .AsNoTracking()
                .Include(d => d.DocRecord)
                .FirstOrDefaultAsync(d => d.DocId== DocId)
                ?? throw new Exception();

            return _mapper.Map<DocRecord>(docEntity);
        }
        public async Task<int> GetLastDocId()
        {
            return await _dbContext.Document.MaxAsync(p => p.Id);
        }
    }
}
