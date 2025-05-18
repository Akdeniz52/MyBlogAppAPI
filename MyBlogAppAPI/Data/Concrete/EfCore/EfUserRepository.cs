
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlogAppAPI.Data.Abstract;
using MyBlogAppAPI.Entity;

namespace MyBlogAppAPI.Data.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public EfUserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IQueryable<User> Users => _userManager.Users;

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        


    }

}