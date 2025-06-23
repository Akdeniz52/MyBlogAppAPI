using System.ComponentModel.DataAnnotations;

namespace MyBlogAppClient.Models
{
    public class RegisterDTO
    {
        [Required]
        [Display(Name ="Kullanıcı Adı")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name="Ad Soyad")]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Eposta")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage ="{0} alanı en az {2} karakter içermelidir. ",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="Parola")]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name ="Parola Tekrar")]
        public string? ConfirmPassword { get; set; }
    }
}