using ClientSVH.Core.Models;

namespace ClientSVH.Core.Abstaction.Repositories
{
    public interface IUsersRepository
    {
        Task<List<User>> GetUsers();
        Task<Guid> Add(User user);
        Task<User> GetByEmail(string email);

        Task<Guid> Update(Guid id, string username, string passwordHash, string email);
        Task<Guid> Delete(Guid id);

    }
}
