using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Models;
using Microsoft.AspNetCore.Http;


namespace ClientSVH.Application.Services
{
    public class UsersService(
        IUsersRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtprovider,
        IHttpContextAccessor httpContextAccessor
           ) : IUsersService
    {
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUsersRepository _usersRepository = userRepository;
        private readonly IJwtProvider _jwtProvider = jwtprovider;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        async Task IUsersService.Register(string username, string password, string email)
        {
            try
            {
                var hasherPassword = _passwordHasher.Generate(password);

                var user = User.Create(Guid.NewGuid(), username, hasherPassword, email);
                
                await _usersRepository.Add(user);
            }
            catch (Exception e)
            {
                throw new Exception("Error code {0}",e);
            }

        }
        async Task<string> IUsersService.Login(string password, string email)
        {
            var user = await _usersRepository.GetByEmail(email) ?? throw new Exception("Invalid username");
            var result = _passwordHasher.Verify(password, user.PasswordHash);
            if (result == false)
            {
                throw new Exception("failed to login");
            }
            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public IEnumerable<Guid> GetUserId()
        {
            var UserId = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == CustomClaims.UserId);
            if (UserId is null || Guid.TryParse(UserId.Value, out var userId))
            {
                throw new Exception("failed to login");
            }

            yield return userId;
        }

    }
}
