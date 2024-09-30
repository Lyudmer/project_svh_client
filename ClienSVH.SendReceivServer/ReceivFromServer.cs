using ClientSVH.Application.Interfaces;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Repositories;
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

    }
    public class ReceivFromServer(IRabbitMQConsumer rabbitMQConsumer,
            IPackagesRepository pkgRepository, IHistoryPkgRepository historyPkgRepository) : IReceivFromServer
    {
        private readonly IPackagesRepository _pkgRepository = pkgRepository;
        private readonly IRabbitMQConsumer _rabbitMQConsumer = rabbitMQConsumer;
        private readonly IHistoryPkgRepository _historyPkgRepository = historyPkgRepository;
        async Task<int> IReceivFromServer.LoadMessage()
        {
            int stPkg = 0;
            try
            {
                // получить сообщение
               
                var resMess = _rabbitMQConsumer.LoadMessage("StatusPkg");
                if (resMess != null )
                {
                    var resRecord = ParsingMess(resMess);
                    int olsstPkg = _pkgRepository.GetByUUId(resRecord.UUID).Result.StatusId;
                    int Pid = _pkgRepository.GetByUUId(resRecord.UUID).Result.Pid;
                    // поменять статус
                    await _pkgRepository.UpdateStatus(Pid, resRecord.Status);
                    // добавить в историю
                    var hPkg = HistoryPkg.Create(Guid.NewGuid(), Pid, olsstPkg, resRecord.Status, "LoadStatusFromServer", DateTime.Now);
                    await _historyPkgRepository.Add(hPkg);
                }
                // создать документ - если пришел документ,

               
            }
            catch (Exception)
            {
                //string mess = ex.Message;

            }
           return stPkg;
        }

        private static ResultMessage ParsingMess(string resMess)
        {
            var resload = new ResultMessage();
            try
            {
                XDocument xMess = XDocument.Load(resMess);
                var xRes = xMess.Element("Result")?.Elements("*");
                resload.Pid = ConverterValue.ConvertTo<int>(xMess.Elements("Result")?.Attributes("pid").ToString());
                resload.UUID = ConverterValue.ConvertTo<Guid>((xRes?.Elements().FirstOrDefault(n => n.Name == "uuid")?.Value is not null).ToString());
                resload.Status = ConverterValue.ConvertTo<int>((xRes?.Elements().FirstOrDefault(n => n.Name == "Status")?.Value is not null).ToString());
                resload.Message = (xRes?.Elements().FirstOrDefault(n => n.Name == "Message")?.Value is not null).ToString() ;

            }
            catch (Exception ex)
            {

                resload.Message =ex.Message;
            }
            return resload;
        }
    }
}
