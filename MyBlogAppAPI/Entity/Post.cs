namespace MyBlogAppAPI.Entity
{
    public class Post
    {
        public int PostId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public DateTime PublishedOn { get; set; }

        public User User { get; set; } = null!;

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}