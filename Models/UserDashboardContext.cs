using Microsoft.EntityFrameworkCore;

namespace UserDashboard.Models{
    public class UserDashboardContext : DbContext{
        public UserDashboardContext(DbContextOptions<UserDashboardContext> options) : base(options) { }
        public DbSet<User> Users  {get; set;}
        public DbSet<Comment> Comments {get;set;}
        public DbSet<Message> Messages {get;set;}
    }
}