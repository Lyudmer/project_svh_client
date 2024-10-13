using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Globalization;
using System.Security.Claims;


namespace ClientSVH.Application.Services
{
    public class UsersService(
        IUsersRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtprovider
           ) : IUsersService
    {
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUsersRepository _usersRepository = userRepository;
        private readonly IJwtProvider _jwtProvider = jwtprovider;
        

        public async Task Register(string username, string password, string email)
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

        public async Task<string> Login(string password, string email)
        {
            var user = await _usersRepository.GetByEmail(email);
            if (user.PasswordHash.Length>0)
            {
                var result = _passwordHasher.Verify(password, user.PasswordHash);
                if (result == false)
                {
                    throw new Exception("failed to login");
                }
              
                var token = _jwtProvider.GenerateToken(user.Id);
                var resUser = GetUserId(token);

                return resUser;
            }
            else throw new Exception("failed to login");
        }

        private string GetUserId(string token)
        {
            var UserId =  _jwtProvider.ReadToken(token);
            if (UserId.Length==0 || !Guid.TryParse(UserId, out var userId))
            {
                throw new Exception("failed to login");
            }

           return userId.ToString();
        }

    }
}
