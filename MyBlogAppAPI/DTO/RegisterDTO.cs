namespace MyBlogAppAPI.DTO
{
    public class RegisterDTO
    {
        public string UserName { get; set; } = null!;

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;


    }
}