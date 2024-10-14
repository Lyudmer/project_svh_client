using ClientSVH.Core.Models;


namespace ClientSVH.Core.Abstraction.Repositories
{
    public interface IStatusRepositoty
    {
        Task<int> Add(Status status);
        Task Delete(int Id);
        Task Update(Status status);
        Task<Status> GetById(int Id);
    }
}