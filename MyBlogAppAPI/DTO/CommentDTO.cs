namespace MyBlogAppAPI.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? Image { get; set; }
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }

}