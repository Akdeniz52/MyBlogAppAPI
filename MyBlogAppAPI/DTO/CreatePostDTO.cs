namespace MyBlogAppAPI.DTOs
{
    public class CreatePostDTO
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; }= null!;
        public string Url { get; set; }= null!;
        public bool IsActive { get; set; }
        public string Description { get; set; } = null!;

        public IFormFile? Image { get; set; }
       
    }
}
