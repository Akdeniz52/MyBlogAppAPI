using System.ComponentModel.DataAnnotations;

namespace MyBlogAppClient.Models
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Eposta")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage ="{0} alanı en az {2} karakter içermelidir. ",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="Parola")]
        public string? Password { get; set; }
    }
}