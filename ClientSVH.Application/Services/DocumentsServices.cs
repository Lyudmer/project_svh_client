using ClientSVH.Application.CollectingListToXml.Hendlers;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Models;
using ClientSVH.DocsRecordCore.Abstraction;
using ClientSVH.DocsRecordCore.Models;
using System.Xml.Linq;



namespace ClientSVH.Application.Services
{
    public class DocumentsServices(IDocumentsRepository documentsRepository,
                                   IDocRecordRepository docRecordRepository) : IDocumentsServices
    {
        private readonly IDocumentsRepository _documentsRepository = documentsRepository;
        private readonly IDocRecordRepository _docRecordRepository = docRecordRepository;
        public async Task<Document> GetDocId(int Id)
        {
            return await _documentsRepository.GetById(Id);
        }
        public async Task<DocRecord> GetDocRecord(int Id)
        {
           var rDoc = await _documentsRepository.GetById(Id);
           return  await _docRecordRepository.GetByDocId(rDoc.DocId);
        }
        public async Task<bool> DeleteDoc(int Id)
        {
            try
            {
                var rDoc = await _documentsRepository.GetById(Id);
                if (rDoc != null)
                {
                    var dRecord = await _docRecordRepository.GetByDocId(rDoc.DocId);
                    if (dRecord != null)
                        await _docRecordRepository.DeleteId(dRecord.DocId);
                    await _documentsRepository.Delete(Id);
                }
                return true;
            }
            catch
            { 
                return false;
            }
            
        }
       
    }
}
