using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly UserManager<User> _userManager;
        public UserController(IUserRepository userRepository,
                             SignInManager<User> signInManager,
                             RoleManager<IdentityRole> roleManager,
                             UserManager<User> userManager,
                             IConfiguration configuration)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO model, IFormFile Image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userRepository.Users
                .FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email);

            if (existingUser != null)
                return BadRequest(new { message = "Kullanıcı adı veya Email kullanımda" });

            string imagePath = null;
            if (Image != null && Image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var savePath = Path.Combine("wwwroot", "uploads", "users", fileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                imagePath = "http://localhost:5261/uploads/users/" + fileName;
            }

            var newUser = new User
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                Image = imagePath
            };

            var result = await _userRepository.CreateUserAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("admin"))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("admin"));
                    if (!roleResult.Succeeded)
                    {
                        return BadRequest("Admin rolü oluşturulamadı");
                    }
                }

                await _userManager.AddToRoleAsync(newUser, "admin");
                return Ok(new { message = "Kayıt başarılı" });
            }

            return BadRequest(new { message = "Kayıt başarısız" });
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
                token = GenerateJWT(user),

            });
        }

        private object GenerateJWT(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? "");

            var baseImageUrl = _configuration["AppSettings:BaseImageUrl"];
            var imageUrl = string.IsNullOrEmpty(user.Image)
                ? ""
                : $"{baseImageUrl}{user.Image}";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim("picture",imageUrl ?? ""),

            };


            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [Authorize]
        [HttpGet("profile/{username}")]
        public IActionResult Profile(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest();

            var user = _userRepository.Users
                        .Include(u => u.Posts)
                        .Include(u => u.Comments)
                            .ThenInclude(c => c.Post)
                        .FirstOrDefault(u => u.UserName == username);

            if (user == null)
                return NotFound();

            var result = new
            {
                user.UserName,
                user.Email,
                user.FullName,
                user.Image,
                Posts = user.Posts.Select(p => new
                {
                    p.PostId,
                    p.Title,
                }),
                Comments = user.Comments.Select(c => new
                {
                    c.CommentId,
                    c.Text,
                    Post = new
                    {
                        c.Post.PostId,
                        c.Post.Title,
                    }
                })
            };

            return Ok(result);
        }
        [Authorize]
        [HttpGet("roles/{username}")]
        public async Task<IActionResult> GetUserRoles(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }
        
        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromForm] EditUserDTO model, IFormFile? Image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

           
            var existingUser = await _userRepository.Users
                .FirstOrDefaultAsync(u => (u.UserName == model.UserName || u.Email == model.Email) && u.Id != userId);

            if (existingUser != null)
                return BadRequest(new { message = "Kullanıcı adı veya email başka bir kullanıcı tarafından kullanılıyor." });

            
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FullName = model.FullName;

            
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (!result.Succeeded)
                    return BadRequest(new { message = "Şifre güncellenemedi", errors = result.Errors });
            }

           
            if (Image != null && Image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var savePath = Path.Combine("wwwroot", "uploads", "users", fileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                user.Image = "http://localhost:5261/uploads/users/" + fileName;
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                return Ok(new { message = "Profil başarıyla güncellendi" });
            }

            return BadRequest(new { message = "Güncelleme sırasında hata oluştu", errors = updateResult.Errors });
        }

    }
}