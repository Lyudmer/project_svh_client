
using ClientSVH.Application.Common;
using ClientSVH.Application.Interfaces;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;

using ClientSVH.DocsRecordCore.Abstraction;
using ClientSVH.DocsRecordCore.Models;
using ClientSVH.SendReceivServer.Consumer;
using System.Xml.Linq;
namespace ClientSVH.SendReceivServer
{

    public class ResultMessage
    {
        public Guid UUID { get; set; }
        public int Pid { get; set; }
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public string DocRecord { get; set; } = string.Empty;

    }
    public class ReceivFromServer(IRabbitMQConsumer rabbitMQConsumer,
            IPackagesRepository pkgRepository, IDocumentsRepository docRepository,
            IDocRecordRepository docRecordRepository,
            IHistoryPkgRepository historyPkgRepository) : IReceivFromServer
    {
        private readonly IPackagesRepository _pkgRepository = pkgRepository;
        private readonly IRabbitMQConsumer _rabbitMQConsumer = rabbitMQConsumer;
        private readonly IHistoryPkgRepository _historyPkgRepository = historyPkgRepository;
        private readonly IDocumentsRepository _docRepository = docRepository;
        private readonly IDocRecordRepository _docRecordRepository = docRecordRepository;
        async Task<int> IReceivFromServer.LoadMessage()
        {
            int stPkg = 0;
            try
            {
                // получить сообщение

                var resMess = _rabbitMQConsumer.LoadMessage("StatusPkg");

                if (resMess != null)
                {
                    await LoadResultFormSerever(resMess, "LoadStatusFromServer");
                    
                }
                // создать документ - если пришел документ,
                var resMessDoc = _rabbitMQConsumer.LoadMessage("DocResultPkg");
                if (resMessDoc != null)
                {
                    var resRecord = ParsingMess(resMessDoc, "Package");
                    int olsstPkg = _pkgRepository.GetByUUId(resRecord.UUID).Result.StatusId;
                    int Pid = _pkgRepository.GetByUUId(resRecord.UUID).Result.Pid;
                    // поменять статус
                    await _pkgRepository.UpdateStatus(Pid, resRecord.Status);
                    // добавить в историю
                    var hPkg = HistoryPkg.Create(Guid.NewGuid(), Pid, olsstPkg, resRecord.Status, "LoadStatusFromServer", DateTime.Now);
                   
                    await _historyPkgRepository.Add(hPkg);
                    // добавить документ
                    if (resRecord.DocRecord != null && await AddDocResPackage(resRecord))
                    {
                        hPkg = HistoryPkg.Create(Guid.NewGuid(), Pid, olsstPkg, resRecord.Status, "Add ConfirmWHDocReg", DateTime.Now);
                        await _historyPkgRepository.Add(hPkg);
                    }
                }
                var resMessDel = _rabbitMQConsumer.LoadMessage("DeletedPkg");
                if (resMessDoc != null)
                {
                    await LoadResultFormSerever(resMessDel, "Del");
                }
            }
            catch (Exception)
            {
                //string mess = ex.Message;

            }
           return stPkg;
        }

        private async Task LoadResultFormSerever(string resMess,string typeMessage)
        {
            var resRecord = ParsingMess(resMess, "Result");
            int olsstPkg = _pkgRepository.GetByUUId(resRecord.UUID).Result.StatusId;
            int Pid = _pkgRepository.GetByUUId(resRecord.UUID).Result.Pid;
            // поменять статус
            await _pkgRepository.UpdateStatus(Pid, resRecord.Status);
            // добавить в историю
            string sMess;
            if (!typeMessage.Contains("Del")) sMess = resRecord.Message;
            else sMess = "LoadStatusFromServer";
            var hPkg = HistoryPkg.Create(Guid.NewGuid(), Pid, olsstPkg, resRecord.Status, sMess, DateTime.Now);
            await _historyPkgRepository.Add(hPkg);
        }

        private static ResultMessage ParsingMess(string resMess,string nodeDoc)
        {
            var resload = new ResultMessage();
            try
            {
                XDocument xMess = XDocument.Load(resMess);
                
                var xRes = xMess.Element(nodeDoc)?.Elements("package-properties");
                if (xRes != null)
                {
                    resload.Pid = ConverterValue.ConvertTo<int>(xMess.Elements(nodeDoc)?.Attributes("pid").ToString());
                    resload.UUID = ConverterValue.ConvertTo<Guid>(xRes?.Elements("name").Where(s => s.Attribute("uuid")?.Value is not null).ToString());
                    resload.Status = ConverterValue.ConvertTo<int>(xRes?.Elements("name").Where(s => s.Attribute("Status")?.Value is not null).ToString());
                    var MessRes= xRes?.Elements("name").Where(s => s.Attribute("Message")?.Value is not null).ToString();
                    if(MessRes?.Length>0) resload.Message = MessRes;
                }

                if (nodeDoc.Contains("Package"))
                {
                    var resDoc = xMess.Element(nodeDoc)?.Elements("CONFIRMWHDOCREGTYPE").ToString();
                    if (resDoc != null && resDoc.Length>0) resload.DocRecord = resDoc;

                }
            }
            catch (Exception ex)
            {

                resload.Message =ex.Message;
            }
            return resload;
        }
        private async Task<bool> AddDocResPackage(ResultMessage resRecord)
        {
            try
            {
                XDocument xDoc = XDocument.Load(resRecord.DocRecord);
                if (xDoc != null)
                {
                    var doc_1 = await _docRepository.GetLastDocId() + 1;
                    var docDate = ConverterValue.ConvertTo<DateTime>((xDoc.Elements().Elements().FirstOrDefault(n => n.Name == "RegDate")?.Value is not null).ToString());
                    var doctext = xDoc.ToString();
                    var docNum = (xDoc.Elements().Elements().FirstOrDefault(n => n.Name == "RegNum")?.Value is not null).ToString();



                    var Doc = Document.Create(doc_1, Guid.NewGuid(), docNum, docDate, "", "ConfirmWHDocReg.cfg.xml", doctext.Length,
                                              DopFunction.GetHashMd5(doctext), DopFunction.GetSha256(doctext),
                                              resRecord.Pid, DateTime.Now, DateTime.Now);


                    Doc = await _docRepository.Add(Doc);
                    if (Doc is not null)
                    {
                        DocRecord dRecord = DocRecord.Create(Guid.NewGuid(), Doc.DocId, doctext, DateTime.Now, DateTime.Now);
                        var dRecordId = await _docRecordRepository.AddRecord(dRecord);

                    }
                    
                }
            }
            catch 
            {
                return false;
            };
            return true;
        }
    }
}
