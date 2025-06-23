using System.Text.Json.Serialization;

namespace MyBlogAppClient.Models
{

    public class PostDetailDTO
    {
        [JsonPropertyName("postId")]
        public int PostId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;
        [JsonPropertyName("content")]
        public string Content { get; set; } = null!;
        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; } = null!;
        [JsonPropertyName("publishedOn")]
        public DateTime PublishedOn { get; set; }
        [JsonPropertyName("authorId")]
        public string AuthorId { get; set; } = null!;
        [JsonPropertyName("authorUserName")]
        public string AuthorUserName { get; set; } = null!;
        [JsonPropertyName("authorFullName")]
        public string AuthorFullName { get; set; } = null!;
        [JsonPropertyName("comments")]
        
        public List<CommentDTO> Comments { get; set; }= new();
    }
}
