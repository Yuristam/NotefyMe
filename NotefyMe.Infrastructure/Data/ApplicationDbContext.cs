using Microsoft.EntityFrameworkCore;
using NotefyMe.Domain.Entities;

namespace NotefyMe.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
            
        }

        public DbSet<Note> Notes { get; set; }
    }
}
