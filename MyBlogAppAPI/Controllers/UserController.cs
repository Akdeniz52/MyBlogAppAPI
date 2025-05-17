using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogAppAPI.Data.Abstract;
using MyBlogAppAPI.DTO;
using MyBlogAppAPI.Entity;

namespace MyBlogAppAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;

        public UserController(IUserRepository userRepository, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepository.Users
                .FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email);

            if (existingUser != null)
                return BadRequest(new { message = "Kullanıcı adı veya Email kullanımda" });

            var newUser = new User
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
            };
            var result = await _userRepository.CreateUserAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { message = "Kayıt başarılı" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepository.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Kullanıcı bulunamadı" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { message = "Geçersiz şifre" });
            }

            return Ok(new
            {
                message = "Giriş başarılı",

            });
        }
        [HttpGet("profile/{username}")]
        public IActionResult Profile(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest();

            var user = _userRepository.Users
                        .Include(x => x.Posts)
                        .Include(x => x.Comments)
                        .ThenInclude(x => x.Post)
                        .FirstOrDefault(x => x.UserName == username);
            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.UserName,
                user.Email,
                user.FullName,
                user.Image
            });
        }
    }
}