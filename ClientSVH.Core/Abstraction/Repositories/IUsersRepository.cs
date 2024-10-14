using ClientSVH.Core.Models;

namespace ClientSVH.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<Guid> Add(User user);
        Task<Guid> Delete(Guid id);
        Task<User> GetByEmail(string email);
        Task<List<User>> GetUsers();
        Task<Guid> Update(User user);
    }
}