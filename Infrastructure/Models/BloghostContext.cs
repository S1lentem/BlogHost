using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    public class BloghostContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CommentModel> Сomments { get; set; }
        public DbSet<TagModel> Tags { get; set; }

        public BloghostContext(DbContextOptions<BloghostContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            string moderatorName = "moderator";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            Role moderatorRole = new Role { Id = 3, Name = moderatorName };
        
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, moderatorRole });
            modelBuilder.Entity<PostTagModel>().HasKey(m => new { m.PostId, m.TagId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
