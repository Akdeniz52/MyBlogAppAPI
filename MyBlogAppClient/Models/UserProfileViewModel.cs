namespace MyBlogAppClient.Models
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public List<PostViewModel> Posts { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }


}