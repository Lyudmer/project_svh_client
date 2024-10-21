using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
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
        

        public async Task<string> Register(string username, string password, string email)
        {
            var resId=Guid.NewGuid();
            try
            {
                var hasherPassword = _passwordHasher.Generate(password);
                resId = await _usersRepository.Add(User.Create(Guid.NewGuid(), username, hasherPassword, email));
                
            }
            catch (Exception)
            {
                return $"Ошибка регистрации";
            }
            if (resId != Guid.Empty)
                return $"Id {resId}";
            else return $"Ошибка регистрации"; 
        }

        public async Task<string> Login(string password, string email)
        {
            var user = await _usersRepository.GetByEmail(email);
            if (user.PasswordHash.Length>0)
            {
                var result = _passwordHasher.Verify(password, user.PasswordHash);
                if (result == false)
                {
                    return $"Пользователь не найден";
                }
              
                var token = _jwtProvider.GenerateToken(user.Id);
                var resUser = GetUserId(token);

                return resUser;
            }
            else return $"Пользователь не найден";
        }

        private string GetUserId(string token)
        {
            var UserId =  _jwtProvider.ReadToken(token);
            if (UserId.Length==0 || !Guid.TryParse(UserId, out var userId))
            {
                return string.Empty;
            }

           return userId.ToString();
        }

    }
}
