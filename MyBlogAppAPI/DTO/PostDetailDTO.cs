namespace MyBlogAppAPI.DTO
{

    public class PostDetailDTO
    {
        public int PostId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Url { get; set; } = null!;
        public bool IsActive { get; set; }
        public string Image { get; set; } = null!;
        public DateTime PublishedOn { get; set; }

        // Eklenen alanlar
        public string AuthorId { get; set; } = null!;
        public string AuthorUserName { get; set; } = null!;
        public string AuthorFullName { get; set; } = null!;
        
        public List<CommentDTO>? Comments { get; set; }
    }
}
