using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly AuthDBContext _context;

        public UserRepository(AuthDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetallUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

    }
}
