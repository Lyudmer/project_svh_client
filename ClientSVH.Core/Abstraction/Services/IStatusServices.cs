using ClientSVH.Core.Models;

namespace ClientSVH.Core.Abstraction.Services
{
    public interface IStatusServices
    {
        Task<int> AddStatus(Status status);
        Task<bool> DelStatus(int Id);
    }
}