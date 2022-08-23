using System.Linq;
using Microsoft.EntityFrameworkCore;
using Numito_Blog.DataLayer.Entities;

namespace Numito_Blog.DataLayer.Context
{
    public class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options):base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s=>s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
