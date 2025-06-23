using System.Text.Json.Serialization;

namespace MyBlogAppClient.Models
{
    public class PostDTO
    {
        
        [JsonPropertyName("postId")]
        public int PostId { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("content")]
        public string? Content { get; set; }
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
        [JsonPropertyName("publishedOn")]
        public DateTime PublishedOn { get; set; }
    }
}