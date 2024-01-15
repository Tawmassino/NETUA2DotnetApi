using Microsoft.EntityFrameworkCore;
using P07_UnitTestingUzduotis;
using P07_UnitTestingUzduotis.Models;

namespace P07_UnitTesting.Database
{
    public class MyBlogContext : DbContext
    {
        public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost { Id = 1, Title = "First Post", Content = "Content of the first post", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new BlogPost { Id = 2, Title = "Second Post", Content = "Content of the second post", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new BlogPost { Id = 3, Title = "Third Post", Content = "Content of the third post", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new BlogPost { Id = 4, Title = "Fourth Post", Content = "Content of the fourth post", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new BlogPost { Id = 5, Title = "Fifth Post", Content = "Content of the fifth post", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );


        }



    }
}
