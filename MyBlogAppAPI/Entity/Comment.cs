using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogAppAPI.Entity
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string? Text { get; set; }

        public DateTime PublishedOn { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
    }
}