using ClientSVH.Application.Interfaces;
using ClientSVH.Application.Services;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.DocsRecordCore.Abstraction;
using ClientSVH.SendReceivServer.Producer;
using System.Data;
using System.Xml.Linq;
namespace ClientSVH.SendReceivServer
{
    public class SendToServer(IPackagesRepository pkgRepository,
       IDocumentsRepository docRepository, IDocRecordRepository docRecordRepository
        , IMessagePublisher messagePublisher, IHistoryPkgRepository historyPkgRepository
       ) : ISendToServer
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;
        private readonly IDocumentsRepository _docRepository = docRepository;
        private readonly IDocRecordRepository _docRecordRepository = docRecordRepository;
        private readonly IPackagesRepository _pkgRepository = pkgRepository;
        private readonly IHistoryPkgRepository _historyPkgRepository= historyPkgRepository;
        
        async Task<int> ISendToServer.SendPaskageToServer(int Pid)
        {

            int stPkg = _pkgRepository.GetById(Pid).Result.StatusId;
            
            try
            {
                // собрать xml
                var xPkg = await CreatePaskageXml(Pid, stPkg);
                // отправить на сервер 

                var resStatus = _messagePublisher.SendMessage(xPkg, "SendPkg", stPkg);
                if (resStatus != stPkg)
                {
                     
                    await _pkgRepository.UpdateStatus(Pid, resStatus);
                    var hPkg = HistoryPkg.Create( Pid, stPkg, resStatus, "SendPkgToServer", DateTime.Now);
                    await _historyPkgRepository.Add(hPkg);
                    stPkg= resStatus;
                }
            }
            catch (Exception)
            {
                //string mess = ex.Message;

            }
            return stPkg;
        }
        async Task<bool> ISendToServer.SendDelPkgToServer(int Pid)
        {
            try 
            {
                int stPkg = _pkgRepository.GetById(Pid).Result.StatusId;
                var xPkg = new XDocument();
                var elem = new XElement("Package");
                elem.SetAttributeValue("pid", Pid);
                var elem_props = new XElement("package-properties",
                    new XElement("props", new XAttribute("name", "uuid"), _pkgRepository.GetById(Pid).Result.UUID.ToString()));
                elem.Add(elem_props);
                var resDel = _messagePublisher.SendMessage(xPkg, "DelPkg",0);
                if (resDel == -1)
                {
                    await _pkgRepository.UpdateStatus(Pid, 107);
                    var hPkg = HistoryPkg.Create( Pid, stPkg, 107, "SendPkgToServer", DateTime.Now);
                    await _historyPkgRepository.Add(hPkg);
                    
                    return true;
                }
                else
                { 
                    return false;
                }
                
            }
            catch {
                return false;
            }
            
        }
        private async Task<XDocument> CreatePaskageXml(int Pid, int stPkg)
        {
            var xPkg = new XDocument();
            var elem = new XElement("Package");
            elem.SetAttributeValue("pid", Pid);
            var elem_props = new XElement("package-properties" 
                , new XElement("props", new XAttribute("name", "Status"), stPkg.ToString())
                , new XElement("props", new XAttribute("name", "uuid"), _pkgRepository.GetById(Pid).Result.UUID.ToString())
                , new XElement("props", new XAttribute("name", "CreateDate"), _pkgRepository.GetById(Pid).Result.CreateDate.ToString())
                , new XElement("props", new XAttribute("name", "UserId"), _pkgRepository.GetById(Pid).Result.UserId.ToString()));
            elem.Add(elem_props);
            var docs = await _docRepository.GetByFilter(Pid);
            foreach (var docId in docs.AsParallel().Select(d => d.DocId).ToList())
                foreach (var doc in docs)
                {
                    elem.SetAttributeValue("docid", doc.DocId.ToString());
                    elem.SetAttributeValue("doctype", doc.DocType);
                    elem.SetAttributeValue("createdate", doc.CreateDate);
                    elem.Add(_docRecordRepository.GetByDocId(doc.DocId).ToString());
                }
            xPkg.Add(elem);
            return xPkg;
        }

    }
}
