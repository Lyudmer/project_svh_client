
using ClientSVH.DocsBodyCore.Abstraction;
using ClientSVH.DocsBodyCore.Models;

namespace ClientSVH.Application.Services
{
    public class DocRecordServices : IDocRecordServices
    {

        private readonly IDocRecordServices _docRecordService;
        private readonly IDocRecordRepository _docRecordRepository;

        public DocRecordServices(IDocRecordRepository docRecordRepository,
             IDocRecordServices docRecordService)
        {
            _docRecordRepository = docRecordRepository;
            _docRecordService = docRecordService;
        }

        public async Task<DocRecord?> GetId(Guid docId) =>
            await _docRecordRepository.GetByDocId(docId);

        public async Task Add(DocRecord docRecord) =>
            await _docRecordRepository.Add(docRecord);

        public async Task Update(Guid Docid, DocRecord docRecord) =>
           await _docRecordRepository.Update(Docid, docRecord);
        public async Task Delete(Guid Docid) =>
            await _docRecordRepository.DeleteId(Docid);
    }
}
