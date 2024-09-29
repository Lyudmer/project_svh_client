using ClientSVH.Application.Interfaces;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Repositories;
using ClientSVH.SendReceivServer.Consumer;
namespace ClientSVH.SendReceivServer
{
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
                string CodeCMN = "StatusPkg";
                var resMess = _rabbitMQConsumer.LoadMessage(CodeCMN);
                if (resMess != null) 
                {
                    stPkg  = int.Parse(resMess);
                    var Pid = int.Parse(resMess);
                    int olsstPkg = _pkgRepository.GetByStatus(Pid).Result;
                    await _pkgRepository.UpdateStatus(Pid, stPkg);
                    var hPkg = HistoryPkg.Create(Guid.NewGuid(), Pid, olsstPkg, stPkg, "LoadStatusFromServer", DateTime.Now);
                    await _historyPkgRepository.Add(hPkg);

                }
                // создать документ - если пришел документ,

                // поменять статус
            }
            catch (Exception)
            {
                //string mess = ex.Message;

            }
           return stPkg;
        }

      

    }
}
