using Microsoft.EntityFrameworkCore;
using NotefyMe.Application.Interfaces;
using NotefyMe.Domain.Users;
using NotefyMe.Infrastructure.Data;

namespace NotefyMe.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(WebUser user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Delete(WebUser user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<WebUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<WebUser> GetUserId(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(WebUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
