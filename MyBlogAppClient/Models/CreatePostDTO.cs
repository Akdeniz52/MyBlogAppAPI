using System.ComponentModel.DataAnnotations;


namespace MyBlogAppClient.Models
{
    public class CreatePostDTO
    {
        
        [Required]
        [Display(Name ="Başlık")]
        public string? Title { get; set; }
        [Required]
        [Display(Name ="İçerik")]
        public string? Content { get; set; }

        [Required]
        [Display(Name ="Url")]
        public string? Url { get; set; }
        [Required]
        [Display(Name ="Açıklama")]
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
       
      

    }
}