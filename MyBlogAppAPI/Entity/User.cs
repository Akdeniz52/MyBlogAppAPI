using Microsoft.AspNetCore.Identity;

namespace MyBlogAppAPI.Entity
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }

        public string? Image { get; set; }

        public List<Comment> Comments { get; set; } = null!;

        public List<Post> Posts { get; set; } = null!;
    }
}