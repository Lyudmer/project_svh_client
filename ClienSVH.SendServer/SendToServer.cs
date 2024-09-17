using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.DocsBodyCore.Abstraction;
using System.Data;

namespace ClientSVH.SendServer
{
    public class SendToServer(
       IDocumentsServices docServices, IDocRecordServices docRecordServices
       ) :ISendToServer
    {
        
        private readonly IDocumentsServices _docServices = docServices;
        private readonly IDocRecordServices _docRecordServices = docRecordServices;


        public async Task<int> SendPaskageToServer(int Pid)
        {

            int stPkg = 0;
            try 
            {
                
                var docs = await _docServices.GetByFilter(Pid);
                var docRec = new List<string>();
                //foreach (var docId in docs.AsParallel().Select(d => d.DocId).ToList())
                //{
                //    docId.ToString();
                //     _docRecordServices.GetId(docId).ToString();
                //}

                //foreach (var docId in docs.AsParallel().Select(d => d.DocId).ToList())
                //    foreach (var doc in docs)
                //{
                //    _docRecordServices.GetId(doc.DocId).ToString();
                //}
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
            }
            return stPkg;
        }
    }
}
