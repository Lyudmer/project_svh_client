using ClientSVH.Core.Models;


using Microsoft.EntityFrameworkCore;

using ClientSVH.DataAccess.Entities;
using ClientSVH.Core.Abstraction.Repositories;
using AutoMapper;

namespace ClientSVH.DataAccess.Repositories
{
    public class UsersRepository(ClientSVHDbContext context, IMapper mapper) : IUsersRepository
    {
        private readonly ClientSVHDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<Guid> Add(User user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<User>(userEntity).Id;
        }
        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
            return _mapper.Map<User>(userEntity);
        }

        public async Task<Guid> Update(User user)
        {
            var resId = await _context.Users
                         .Where(u => u.Id == user.Id)
                         .ExecuteUpdateAsync(s => s
                         .SetProperty(u => u.UserName, u => user.UserName)
                         .SetProperty(u => u.PasswordHash, u => user.PasswordHash)
                         .SetProperty(u => u.Email, u => user.Email));
            if (resId > 0) return user.Id;
            else return Guid.Empty;
        }
        public async Task<Guid> Delete(Guid id)
        {
            var resId = await _context.Users
                        .Where(u => u.Id == id)
                        .ExecuteDeleteAsync();
            if (resId > 0) return id;
            else return Guid.Empty;

        }
        public async Task<List<User>> GetUsers()
        {
            var userEntites = await _context.Users
                 .AsNoTracking()
                 .ToListAsync();
            return _mapper.Map<List<User>>(userEntites);
        }

    }

}
