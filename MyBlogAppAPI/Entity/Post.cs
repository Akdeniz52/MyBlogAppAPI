using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogAppAPI.Entity
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Url { get; set; }
        public bool IsActive { get; set; } = false;
        public string? Image { get; set; }
        public DateTime PublishedOn { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}