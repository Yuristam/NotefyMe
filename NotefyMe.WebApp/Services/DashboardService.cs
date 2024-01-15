using Microsoft.EntityFrameworkCore;
using NotefyMe.Application.Services;
using NotefyMe.Domain.Entities;
using NotefyMe.Domain.Users;
using NotefyMe.Infrastructure.Data;
using NotefyMe.Infrastructure.Extensions;

namespace NotefyMe.WebApp.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Note>> GetAllUserNotes()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userNotes = _context.Notes.Where(c => c.WebUser.Id == currentUser);

            return userNotes.ToList();
        }

        public async Task<WebUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<WebUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public bool Update(WebUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}