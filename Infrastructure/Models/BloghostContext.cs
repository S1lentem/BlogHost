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
        public DbSet<RoleModel> Roles { get; set; }

        public BloghostContext(DbContextOptions<BloghostContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "Admin";
            string userRoleName = "User";
            string moderName = "Moder";

            RoleModel adminRole = new RoleModel { Id = 1, Name = adminRoleName };
            RoleModel userRole = new RoleModel { Id = 2, Name = userRoleName };
            RoleModel moderatorRole = new RoleModel { Id = 3, Name = moderName };
        
            modelBuilder.Entity<RoleModel>().HasData(new RoleModel[] { adminRole, userRole, moderatorRole });
            modelBuilder.Entity<PostTagModel>().HasKey(m => new { m.PostId, m.TagId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
