using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotefyMe.Domain.Entities;
using NotefyMe.Domain.Users;

namespace NotefyMe.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<WebUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
            
        }

        public DbSet<Note> Notes { get; set; }
    }
}
