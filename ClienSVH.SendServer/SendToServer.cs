
using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.DocsRecordCore.Abstraction;
using System.Data;
using System.Xml.Linq;

namespace ClientSVH.SendServer
{
    public class SendToServer(
       IDocumentsRepository docRepository, IDocRecordRepository docRecordRepository
        , IMessageProducer messagePublisher
       ) : ISendToServer
    {
        private readonly IMessageProducer _messagePublisher= messagePublisher;
        private readonly IDocumentsRepository _docRepository = docRepository;
        private readonly IDocRecordRepository _docRecordRepository = docRecordRepository;

       
        async Task<int> ISendToServer.SendPaskageToServer(int Pid)
        {

            int stPkg = 0;
            try
            {
// собрать xml
               var xPkg = await CreatePaskageXml(Pid);
                // отправить на сервер 
                _messagePublisher.SendMessage(xPkg,"SendPkg");

            }
            catch (Exception )
            {
                //string mess = ex.Message;

            }
            return stPkg;
        }

        private async Task<XDocument> CreatePaskageXml(int Pid)
        {
           
            var xPkg = new XDocument();

            var elem = new XElement("Package");
            elem.SetAttributeValue("pid", Pid);
            var docs = await _docRepository.GetByFilter(Pid);
            foreach (var docId in docs.AsParallel().Select(d => d.DocId).ToList()) 
                foreach (var doc in docs)
            {
                elem.Add(_docRecordRepository.GetByDocId(doc.DocId).ToString());
            }
            xPkg.Add(elem);
            return xPkg;
        }
    }
}
