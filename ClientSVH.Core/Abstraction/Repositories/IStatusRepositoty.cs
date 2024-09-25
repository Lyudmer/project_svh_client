using ClientSVH.Core.Models;

namespace ClientSVH.Core.Abstraction.Repositories
{
    public interface IStatusRepositoty
    {
        Task<int> Add(int Id, string StatusName, bool RunWf, bool MkRes, bool SendMess);
        Task Delete(int Id);
        Task Update(int Id, string StatusName, bool RunWf, bool MkRes);
        Task<Status> GetById(int Id);
    }
}