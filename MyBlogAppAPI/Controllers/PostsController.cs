using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogAppAPI.Data.Abstract;
using MyBlogAppAPI.DTOs;
using MyBlogAppAPI.Entity;


namespace MyBlogAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet("posts-list")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.Posts
                .Where(x => x.IsActive)
                .ToListAsync();

            return Ok(posts);
        }

        [HttpGet("detailspost-id")]
        public async Task<IActionResult> GetPostDetails(int id)
        {
            var post = await _postRepository.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        
        [Authorize]
        [HttpPost("create-post")]
        public async Task<IActionResult> CreatePost(CreatePostDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
                

            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Description = model.Description,
                Url = model.Url,
                Image = "1.jpg",
                PublishedOn = DateTime.Now,
                UserId = userId,
                IsActive = false
            };

            await _postRepository.CreatePostAsync(post);
            return Ok(post);
        }

        [Authorize]
        [HttpGet("my-post-list")]
        public async Task<IActionResult> GetUserPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            
            var posts = _postRepository.Posts;
            if (userId == null)
            {
                return BadRequest();
            }
            
            return Ok(await posts.Where(p => p.UserId == userId).ToListAsync());
             
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPost(int id, CreatePostDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entityToUpdate = await _postRepository.Posts
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (entityToUpdate == null)
            {
                return NotFound();
            }

            entityToUpdate.Title = model.Title;
            entityToUpdate.Content = model.Content;
            entityToUpdate.Description = model.Description;
            entityToUpdate.Url = model.Url;

            if (User.FindFirstValue(ClaimTypes.Role) == "admin")
            {
                entityToUpdate.IsActive = model.IsActive;
            }

            await _postRepository.EditPostAsync(entityToUpdate);
            return Ok(entityToUpdate);
        }
    }
}
