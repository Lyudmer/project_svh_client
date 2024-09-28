using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.DocsRecordCore.Abstraction;
using ClientSVH.SendServer.Producer;
using System.Data;
using System.Xml.Linq;
namespace ClientSVH.SendReceivServer
{
    public class SendToServer(IPackagesRepository pkgRepository,
       IDocumentsRepository docRepository, IDocRecordRepository docRecordRepository
        , IMessagePublisher messagePublisher
       ) : ISendToServer
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;
        private readonly IDocumentsRepository _docRepository = docRepository;
        private readonly IDocRecordRepository _docRecordRepository = docRecordRepository;
        private readonly IPackagesRepository _pkgRepository = pkgRepository;

        async Task<int> ISendToServer.SendPaskageToServer(int Pid)
        {

            int stPkg = _pkgRepository.GetByStatus(Pid).Result;
            try
            {
                // собрать xml
                var xPkg = await CreatePaskageXml(Pid, stPkg);
                // отправить на сервер 

                _messagePublisher.SendMessage(xPkg, "SendPkg");

            }
            catch (Exception)
            {
                //string mess = ex.Message;

            }
            return stPkg;
        }

        private async Task<XDocument> CreatePaskageXml(int Pid, int stPkg)
        {

            var xPkg = new XDocument();

            var elem = new XElement("Package");
            elem.SetAttributeValue("pid", Pid);

            var elem_props = new XElement("package-properties", new XElement("props", new XAttribute("name", "Status"), stPkg.ToString()));
            elem.Add(elem_props);
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
