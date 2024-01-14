using Microsoft.AspNetCore.Identity;
using NotefyMe.Domain.Entities;

namespace NotefyMe.Domain.Users
{
    public class WebUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
