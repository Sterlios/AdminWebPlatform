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
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId);

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "User",
                    UserAccessLevel = AccessLevel.None,
                    ContentAccessLevel = AccessLevel.None
                },
                new Role
                {
                    Id = 2,
                    Name = "Reader",
                    UserAccessLevel = AccessLevel.None,
                    ContentAccessLevel = AccessLevel.Read
                },
                new Role
                {
                    Id = 3,
                    Name = "ContentModerator",
                    UserAccessLevel = AccessLevel.None,
                    ContentAccessLevel = AccessLevel.Read
                        | AccessLevel.Read
                        | AccessLevel.Edit
                        | AccessLevel.Create
                        | AccessLevel.Delete
                },
                new Role
                {
                    Id = 4,
                    Name = "UserModerator",
                    UserAccessLevel = AccessLevel.Read
                        | AccessLevel.Read
                        | AccessLevel.Edit
                        | AccessLevel.Create
                        | AccessLevel.Delete,
                    ContentAccessLevel = AccessLevel.None,
                },
                new Role
                {
                    Id = 5,
                    Name = "Administrator",
                    UserAccessLevel = AccessLevel.Read
                        | AccessLevel.Read
                        | AccessLevel.Edit
                        | AccessLevel.Create
                        | AccessLevel.Delete,
                    ContentAccessLevel = AccessLevel.Read
                        | AccessLevel.Read
                        | AccessLevel.Edit
                        | AccessLevel.Create
                        | AccessLevel.Delete,
                }
                );
            ;
        }
    }
}
