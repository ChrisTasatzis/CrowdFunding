using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public class PostService : IPostService
    {
        private readonly CFContext _context;

        public PostService(CFContext context)
        {
            _context = context;
        }

        public Post CreatePost(Post post, projectid)
        {
            var project = _context.Projects.Find(projectId);
            if (project == null)
            {
                return null;
            }

            var post = new Post() { Project = project, DateTime=DateTime.Now, Text= text };
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }

        public Post ReadPost (int id)
        {
            return _context.Posts.Find(id);

        }

        public List <Post> ReadPost(projectId, int pageCount, int pageSize)
        {
            Project project = _context.Projects.Find(projectId);
            if (pageCount <= 0) pageCount = 1;
            if (pageSize <= 0 || pageSize > 20) pageSize = 20;
          return _context.Posts
                .Skip(pageCount - 1) * pageSize
                .Take(pageSize)
                .ToList();
        }

        public Post DeletePost(int id)
        {
            var contextPost = _context.Posts.Find(id);
            if (contextPost == null) return false;
            _context.Posts.Remove(contextPost);
            _context.SaveChanges();
            return _context.SaveChanges() == 1;
            
        }

        public Post UpdatePost(int id, Post post)
        {
            var contextPost = _context.Posts.Find(id);
            if (contextPost == null) throw new KeyNotFoundException();
            contextPost.Text = post.Text;
            _context.SaveChanges();
            return post;

        }
    }
}
