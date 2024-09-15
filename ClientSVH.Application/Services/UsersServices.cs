﻿using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstaction.Repositories;
using ClientSVH.Core.Abstaction.Services;
using ClientSVH.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClientSVH.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly AuthorizationHandlerContext _context;
        public UsersService(
            IUsersRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtprovider, 
            AuthorizationHandlerContext context
           )
        {
            _passwordHasher = passwordHasher;
            _usersRepository = userRepository;
            _jwtProvider = jwtprovider;
            _context = context;  
        }

        async Task IUsersService.Register(string username, string password, string email)
        {
            var hasherPassword = _passwordHasher.Generate(password);
            var user = User.Create(Guid.NewGuid(), username, hasherPassword, email);

            await _usersRepository.Add(user);
        }
        async Task<string> IUsersService.Login(string password, string email)
        {
            var user = await _usersRepository.GetByEmail(email);
            var result = _passwordHasher.Verify(password, user.PasswordHash);
            if (result == false)
            {
                throw new Exception("failed to login");
            }
            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public Guid GetUserIdFromLogin()
        {
            var UserClaim =  _context.User.Claims
                .FirstOrDefault(u => u.Type == CustomClaims.UserId);

            bool resId =  Guid.TryParse(UserClaim?.Value, out Guid userId);

            if (!resId)
            {
                throw new Exception("failed to userid");
            }
            return userId;

        }
         
    }
}
