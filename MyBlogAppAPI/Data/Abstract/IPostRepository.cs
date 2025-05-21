using MyBlogAppAPI.Entity;

namespace MyBlogAppAPI.Data.Abstract
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }

        Task CreatePostAsync(Post post);
        Task EditPostAsync(Post post);
        Task SaveChangesAsync();
        Task DeletePost(int postId);

       
    }
}