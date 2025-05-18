using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlogAppAPI.Data.Abstract;
using MyBlogAppAPI.DTO;
using MyBlogAppAPI.Entity;


namespace MyBlogAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(CreateCommentDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            

            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var comment = new Comment
            {
                Text = model.Text,
                PublishedOn = DateTime.Now,
                UserId = userId,
                User = new User { UserName = username, Image = avatar }
            };

            _commentRepository.CreateComment(comment);
            return Ok(comment);
        }
    }
}
