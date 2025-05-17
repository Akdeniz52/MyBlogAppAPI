
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlogAppAPI.Entity;

namespace MyBlogAppAPI.Data.Concrete.EfCore
{
    public class BlogContext : IdentityDbContext<User, IdentityRole, string>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Post> Posts => Set<Post>();
    }
}