

using MyBlogAppAPI.Entity;

namespace MyBlogAppAPI.Data.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments {get;}

        Task CreateCommentAsync(Comment comment);
    }
}