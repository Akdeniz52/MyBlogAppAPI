using System.ComponentModel.DataAnnotations;

namespace MyBlogAppClient.Models
{
    public class EditUserDTO
    {
        [Required]
        public string? UserId { get; set; }
        
        [Display(Name ="Kullanıcı Adı")]
        public string? UserName { get; set; }

        
        [Display(Name="Ad Soyad")]
        public string? FullName { get; set; }

        
        [EmailAddress]
        [Display(Name ="Eposta")]
        public string? Email { get; set; }

        
        [StringLength(10,ErrorMessage ="{0} alanı en az {2} karakter içermelidir. ",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="Parola")]
        public string? Password { get; set; }

        
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name ="Parola Tekrar")]
        public string? ConfirmPassword { get; set; }
    }
}