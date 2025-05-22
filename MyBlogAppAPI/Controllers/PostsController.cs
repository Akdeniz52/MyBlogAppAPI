using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogAppAPI.Data.Abstract;
using MyBlogAppAPI.DTO;
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

        [HttpGet("list")]
        public async Task<IActionResult> GetPosts()
        {
            var postlist = await _postRepository.Posts
                .Where(x => x.IsActive)
                .ToListAsync();

            if (postlist == null || !postlist.Any())
            {
                return NotFound("Gönderi bulunamadı.");
            }

            var postDtoList = postlist.Select(post => new PostDTO
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                IsActive = post.IsActive,
                Image = $"{Request.Scheme}://{Request.Host}/img/{post.Image}",
                PublishedOn = post.PublishedOn
            }).ToList();

            return Ok(postDtoList);
        }

        [HttpGet("details/{id}")]
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

            var dto = new PostDetailDTO
            {
                PostId = post.PostId,
                Title = post.Title ?? "",
                Description = post.Description ?? "",
                Content = post.Content ?? "",
                Url = post.Url ?? "",
                IsActive = post.IsActive,
                Image = $"{Request.Scheme}://{Request.Host}/img/{post.Image}",
                PublishedOn = post.PublishedOn,
                AuthorId = post.User.Id,
                AuthorUserName = post.User.UserName ?? "",
                AuthorFullName = post.User.FullName ?? "",
                Comments = post.Comments.Select(c => new CommentDTO
                {
                    CommentId = c.CommentId,
                    Text = c.Text ?? "",
                    CreatedOn = c.PublishedOn,
                    UserId = c.User.Id,
                    Image = c.User.Image,
                    UserName = c.User.UserName ?? ""
                }).ToList()
            };

            return Ok(dto);
        }



        [Authorize]
        [HttpPost("create-post")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDTO model)
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

            string? imageName = null;
            if (model.Image != null)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                imageName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imageName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }


            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Description = model.Description,
                Url = model.Url,
                Image = imageName ?? "default.jpg",
                PublishedOn = DateTime.Now,
                UserId = userId,
                IsActive = false
            };

            await _postRepository.CreatePostAsync(post);
            return Ok(new { message = "Post oluşturuldu" });
        }

        [Authorize]
        [HttpGet("my-post-list")]
        public async Task<IActionResult> GetUserPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";


            if (userId == null)
            {
                return BadRequest();
            }
            var posts = await _postRepository.Posts.Where(p => p.UserId == userId).ToListAsync();
            if (posts == null || !posts.Any())
            {
                return NotFound("Gönderi bulunamadı.");
            }

            var postDtoList = posts.Select(post => new PostDTO
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                IsActive = post.IsActive,
                Image = $"{Request.Scheme}://{Request.Host}/img/{post.Image}",
                PublishedOn = post.PublishedOn
            }).ToList();


            return Ok(postDtoList);

        }

        [Authorize]
        [HttpPut("edit-post")]
        public async Task<IActionResult> EditPost([FromForm] EditPostDTO model)
        {
            var post = await _postRepository.Posts.FirstOrDefaultAsync(p => p.PostId == model.PostId);
            if (post == null)
                return NotFound();

            post.Title = model.Title;
            post.Content = model.Content;
            post.Description = model.Description;
            post.Url = model.Url;
            post.IsActive = model.IsActive;

            if (model.Image != null)
            {

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                post.Image = fileName;
            }

            await _postRepository.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = await _postRepository.Posts.FirstOrDefaultAsync(p => p.PostId == id && p.UserId == userId);

            if (post == null)
            {
                return NotFound("Post bulunamadı veya silme yetkiniz yok.");
            }

            await _postRepository.DeletePost(post.PostId);
            await _postRepository.SaveChangesAsync();

            return Ok(new { message = "Post başarıyla silindi." });
        }

        [Authorize(Roles ="admin")]
        [HttpGet("adminpanelpost")]
        public async Task<IActionResult> AdminPanelPostAsync()
        {
            var postlist = await _postRepository.Posts
                .ToListAsync();

            if (postlist == null || !postlist.Any())
            {
                return NotFound("Gönderi bulunamadı.");
            }

            var postDtoList = postlist.Select(post => new PostDTO
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                IsActive = post.IsActive,
                Image = $"{Request.Scheme}://{Request.Host}/img/{post.Image}",
                PublishedOn = post.PublishedOn
            }).ToList();

            return Ok(postDtoList);
        }

    }
}
