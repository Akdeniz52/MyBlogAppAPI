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
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                await _userManager.AddToRoleAsync(newUser, "User");
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
                token=GenerateJWT(user),

            });
        }

        private object GenerateJWT(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? "");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
               
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
    }
}