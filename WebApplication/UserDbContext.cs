using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserProfile>().ToTable("UserProfile");
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserProfile Profile { get; set; }
    }

    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Address { get; set; }
    }
}