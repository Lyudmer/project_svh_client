using ClientSVH.Core.Models;

namespace ClientSVH.Core.Abstraction.Services
{
    public interface IStatusServices
    {
        Task<string> AddStatus(Status status);
        Task<string> DelStatus(int Id);
    }
}