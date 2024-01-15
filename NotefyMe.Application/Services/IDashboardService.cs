using NotefyMe.Domain.Entities;
using NotefyMe.Domain.Users;

namespace NotefyMe.Application.Services
{
    public interface IDashboardService
    {
        Task<List<Note>> GetAllUserNotes();
        Task<WebUser> GetUserById(string id);
        Task<WebUser> GetUserByIdNoTracking(string id);
        bool Update(WebUser user);
        bool Save();
    }
}
