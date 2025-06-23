using System.Text.Json.Serialization;

namespace MyBlogAppClient.Models
{
    public class CommentDTO
    {
        [JsonPropertyName("commentId")]
        public int CommentId { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; } = null!;
        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
        [JsonPropertyName("userId")]

        public string UserId { get; set; } = null!;
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = null!;
    }

}