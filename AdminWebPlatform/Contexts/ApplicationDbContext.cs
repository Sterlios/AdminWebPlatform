using AdminWebPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminWebPlatform.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public required DbSet<Role> Roles { get; set; }
    }
}
