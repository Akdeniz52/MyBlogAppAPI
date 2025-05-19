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
        [HttpPost("add")]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var comment = new Comment
            {
                Text = model.Text,
                PublishedOn = DateTime.Now,
                UserId = userId,
                PostId = model.PostId
            };

            await _commentRepository.CreateCommentAsync(comment);

            return Ok(comment);
        }

    }
}
