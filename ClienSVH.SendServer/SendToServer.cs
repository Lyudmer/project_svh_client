
using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.DocsBodyCore.Abstraction;
using System.Data;
using System.Xml.Linq;

namespace ClientSVH.SendServer
{
    public class SendToServer(
       IDocumentsServices docServices, IDocRecordServices docRecordServices
       ) : ISendToServer
    {
        
        private readonly IDocumentsServices _docServices = docServices;
        private readonly IDocRecordServices _docRecordServices = docRecordServices;

       
        async Task<int> ISendToServer.SendPaskageToServer(int Pid)
        {

            int stPkg = 0;
            try
            {
// собрать xml
               var xPkg = await CreatePaskageXml(Pid);
// отправить на сервер 

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
            var docs = await _docServices.GetByFilter(Pid);
            foreach (var docId in docs.AsParallel().Select(d => d.DocId).ToList()) 
                foreach (var doc in docs)
            {
                elem.Add(_docRecordServices.GetId(doc.DocId).ToString());
            }
            
            //foreach (var docId in docs.Select(d => d.DocId).ToList())
            //{
            //    var docRec = _docRecordServices.GetId(docId).ToString();
            //    elem.Add(docRec);
            //}
            xPkg.Add(elem);
            return xPkg;
        }
    }
}
