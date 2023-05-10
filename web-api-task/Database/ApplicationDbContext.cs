using Microsoft.EntityFrameworkCore;
using web_api_task.Models;

namespace web_api_task.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserState> UserStates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>().HasData(
                new UserGroup { Id = 1, Code = "Admin", Description = "Administrator" },
                new UserGroup { Id = 2, Code = "User", Description = "Regular user" }
            );

            modelBuilder.Entity<UserState>().HasData(
                new UserState { Id = 1, Code = "Active", Description = "Active user" },
                new UserState { Id = 2, Code = "Blocked", Description = "Blocked user" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
