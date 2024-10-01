using ClientSVH.Core.Models;

namespace ClientSVH.Core.Abstraction.Repositories
{
    public interface IHistoryPkgRepository
    {
        Task<HistoryPkg> Add(HistoryPkg HpPkg);
        Task<HistoryPkg> GetById(int Pid);
    }
}