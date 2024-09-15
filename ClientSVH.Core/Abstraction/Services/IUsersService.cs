namespace ClientSVH.Core.Abstaction.Services
{
    public interface IUsersService
    {
        Task Register(string username, string password, string email);
        Task<string> Login(string password, string email);
        Guid GetUserIdFromLogin();

    }
}