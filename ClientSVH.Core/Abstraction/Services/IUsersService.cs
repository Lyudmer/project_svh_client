namespace ClientSVH.Core.Abstraction.Services
{
    public interface IUsersService
    {
        Task<string> Register(string username, string password, string email);
        Task<string> Login(string password, string email);
       
    }
}