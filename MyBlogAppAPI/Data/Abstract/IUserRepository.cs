using Microsoft.AspNetCore.Identity;
using MyBlogAppAPI.Entity;

namespace MyBlogAppAPI.Data.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        Task<IdentityResult> CreateUserAsync(User user, string password);

        Task<bool> CheckPasswordAsync(User user, string password);

}

}