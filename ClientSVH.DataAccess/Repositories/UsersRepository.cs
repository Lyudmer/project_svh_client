using ClientSVH.Core.Models;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using ClientSVH.DataAccess.Entities;
using ClientSVH.Core.Abstaction.Repositories;

namespace ClientSVH.DataAccess.Repositories
{
    public class UsersRepository(ClientSVHDbContext context, IMapper mapper) : IUsersRepository
    {
        private readonly ClientSVHDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> Add(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email
            };
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return userEntity.Id;
        }
        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            return _mapper.Map<User>(userEntity);
        }

        public async Task<Guid> Update(Guid id, string username, string password, string email)
        {
            await _context.Users
                 .Where(u => u.Id == id)
                 .ExecuteUpdateAsync(s => s
                     .SetProperty(u => u.UserName, u => username)
                     .SetProperty(u => u.PasswordHash, u => password)
                     .SetProperty(u => u.Email, u => email)
                 );
            return id;
        }
        public async Task<Guid> Delete(Guid id)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();
            return id;
        }
        public async Task<List<User>> GetUsers()
        {
            var userEntites = await _context.Users
                 .AsNoTracking()
                 .ToListAsync();
            var users = userEntites
                .Select(u => User.Create(u.Id, u.UserName, u.PasswordHash, u.Email))
                .ToList();
            return users;
        }

    }

}
