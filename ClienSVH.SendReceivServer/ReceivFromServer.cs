
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
        public async Task<int> LoadMessage()
        {

            // получить сообщение с пакетом
            //    var resMessEmul = _rabbitMQConsumer.LoadMessage("EmulSendDoc");
            //var resMess = _rabbitMQConsumer.LoadMessage("statuspkg");
            var resMess = _rabbitMQConsumer.LoadMessage("sendpkg");
            if (resMess != null && resMess.Length > 0)
            {
                return await LoadMessageFile(resMess, "sedpkg");
            }
            return 0;
        }
        public async Task<int> LoadMessageFile(string resMess, string typeMess)
        {
           
            int stPkg = 0;
            try
            {
                if (resMess != null && resMess.Length > 0)
                {
                    // получить сообщение
                    if (resMess != null && resMess.Length > 0)
                    {
                        await LoadResultFormSerever(resMess, "LoadStatusFromServer");

                    }
                    // создать документ - если пришел документ,
                    var resMessDoc = _rabbitMQConsumer.LoadMessage("DocResultPkg");
                    if (resMessDoc != null)
                    {
                        var resRecord = ParsingMess(resMessDoc, "Package");
                        int olsstPkg = _pkgRepository.GetByUUId(resRecord.UUID).Result.StatusId;
                        int Pid = _pkgRepository.GetByUUId(resRecord.UUID).Result.Id;
                        // поменять статус
                        await _pkgRepository.UpdateStatus(Pid, resRecord.Status);
                        // добавить в историю
                        var hPkg = HistoryPkg.Create(Pid, olsstPkg, resRecord.Status, "LoadStatusFromServer", DateTime.UtcNow);

                        await _historyPkgRepository.Add(hPkg);
                        // добавить документ
                        if (resRecord.DocRecord != null && await AddDocResPackage(resRecord))
                        {
                            hPkg = HistoryPkg.Create(Pid, olsstPkg, resRecord.Status, "Add ConfirmWHDocReg", DateTime.UtcNow);
                            await _historyPkgRepository.Add(hPkg);
                        }
                    }
                    var resMessDel = _rabbitMQConsumer.LoadMessage("DeletedPkg");
                    if (resMessDoc != null)
                    {
                        await LoadResultFormSerever(resMessDel, "Del");
                    }
                } else 
                    stPkg = 0;
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
            int Pid = _pkgRepository.GetByUUId(resRecord.UUID).Result.Id;
            // поменять статус
            await _pkgRepository.UpdateStatus(Pid, resRecord.Status);
            // добавить в историю
            string sMess;
            if (!typeMessage.Contains("Del")) sMess = resRecord.Message;
            else sMess = "LoadStatusFromServer";
            var hPkg = HistoryPkg.Create( Pid, olsstPkg, resRecord.Status, sMess, DateTime.UtcNow);
            await _historyPkgRepository.Add(hPkg);
        }

        private static ResultMessage ParsingMess(string resMess,string nodeDoc)
        {
            var resload = new ResultMessage();
            try
            {
                XDocument xMess = XDocument.Parse(resMess);
                
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
                   
                    var docDate = ConverterValue.ConvertTo<DateTime>((xDoc.Elements().Elements().FirstOrDefault(n => n.Name == "RegDate")?.Value is not null).ToString());
                    var doctext = xDoc.ToString();
                    var docNum = (xDoc.Elements().Elements().FirstOrDefault(n => n.Name == "RegNum")?.Value is not null).ToString();

                    var LastDocId = _docRepository.GetLastDocId().Result + 1;

                    var Doc = Document.Create(LastDocId,Guid.NewGuid(), docNum, docDate, "09999", "ConfirmWHDocReg.cfg.xml", doctext.Length,
                                              DopFunction.GetHashMd5(doctext), DopFunction.GetSha256(doctext),
                                              resRecord.Pid, DateTime.Now, DateTime.Now);


                    Doc = await _docRepository.Add(Doc);
                    if (Doc is not null)
                    {
                        DocRecord dRecord = DocRecord.Create( Doc.DocId.ToString(), doctext);
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
