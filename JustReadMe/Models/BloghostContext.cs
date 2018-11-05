using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JustReadMe.Models
{
    public class BloghostContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<PostModel> BlogArticles { get; set; }
        public DbSet<CommentModel> Сomments { get; set; }


        public BloghostContext(DbContextOptions<BloghostContext> options) : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            base.OnModelCreating(modelBuilder);
        }
    }
}
