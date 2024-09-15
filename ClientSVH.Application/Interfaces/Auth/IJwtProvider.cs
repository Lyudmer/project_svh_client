using ClientSVH.Core.Models;

namespace ClientSVH.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}