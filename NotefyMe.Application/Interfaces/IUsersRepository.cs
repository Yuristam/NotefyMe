using NotefyMe.Domain.Users;

namespace NotefyMe.Application.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<WebUser>> GetAllUsers();
        Task<WebUser> GetUserId(string id);
        bool Add(WebUser user);
        bool Update(WebUser user);
        bool Delete(WebUser user);
        bool Save();
    }
}
