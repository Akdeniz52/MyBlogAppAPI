

using MyBlogAppAPI.Entity;

namespace MyBlogAppAPI.Data.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }

        Task CreateCommentAsync(Comment comment);
        
        Task<Comment> GetByIdAsync(int id);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(Comment comment);

    }
}