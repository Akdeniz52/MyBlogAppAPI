

using Microsoft.Net.Http.Headers;
using MyBlogAppAPI.Data.Abstract;
using MyBlogAppAPI.Data.Concrete.EfCore;
using MyBlogAppAPI.Entity;

namespace MyBlogAppAPI.Data.Concrete.EfCore
{
    public class EfCommentRepository:ICommentRepository
    {
        private BlogContext _context;

        public EfCommentRepository(BlogContext context)
        {
            _context=context;
        }

        public IQueryable<Comment> Comments => _context.Comments;

        public void CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}