using web.Entities;
using web.Data;
using web.Helpers;
using web.Models;
using AutoMapper;

namespace web.Services
{
    public interface IPostService 
    {
        public List<Post> GetPosts();
        public List<Post> GetUserPosts(int id);
        public Post GetPost(int id);
        public void CreatePost(Post post);
        public void DeletePost(int id);
        public void UpdatePost(PostModel model);
    }

    public class PostService : IPostService 
    {
        private DatabaseContext context;
        private IJwtUtils jwtUtils;
        public IMapper mapper;
        public PostService(DatabaseContext context, IMapper mapper, IJwtUtils jwtUtils) 
        {
            this.context = context;
            this.mapper = mapper;
            this.jwtUtils = jwtUtils;
        }

        public List<Post> GetPosts() 
        {
            return context.Posts.ToList();
        }
        public List<Post> GetUserPosts(int id) 
        {
            List<Post> userPosts = new List<Post>();
            foreach (Post p in context.Posts.ToList()) 
            {
                if (p.AuthorId == id) {
                    userPosts.Add(p);
                }
            }
            return userPosts;
        }
        public Post GetPost(int id) 
        {
            var post = context.Posts.SingleOrDefault(p => p.Id == id);
            if (post == null) 
            {
                throw new ApplicationException("Post either doesn't have an author or it doesn't exist anymore!");
            }
            return post;
        }  
        public void CreatePost(Post post)
        {
            var user = context.Users.SingleOrDefault(u => u.Id == post.AuthorId);
            post.CreatedAt = DateTime.Now;
            post.Author = user;
            context.Posts.Add(post);
            context.SaveChanges();
        }
        public void DeletePost(int id) 
        {
            var post = context.Posts.SingleOrDefault(p => p.Id == id);
            if (post == null) 
            {
                throw new ApplicationException("Post either doesn't have an author or it doesn't exist anymore!");
            } 
            context.Posts.Remove(post);
            context.SaveChanges();
        }
        public void UpdatePost(PostModel model) {
            var entity = context.Posts.Find(model.Id);
            if (entity == null) 
            {
                throw new ApplicationException("Post doesn't exist!");
            }
            context.Entry(entity).CurrentValues.SetValues(model);
            context.SaveChanges();
        }
    }
}