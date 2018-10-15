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
        public DbSet<BlogArticleModel> BlogArticles { get; set; }

        public BloghostContext(DbContextOptions<BloghostContext> options) : base(options) => Database.EnsureCreated();

    }
}
