using CrowdFunding.DTO;
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

        public Responce<Post> CreatePost(Post post, int projectId)

        { if (post == null || projectId < 0)
            {
                return new Response<Post>()
                {
                    Data = null,
                    Description = "Post was null, please try again",
                    StatusCode = 50
                };
                post.Project = _context.Projects.Find(projectId);
                _context.Posts.Add(post);
                if (_context.SaveChanges()==1);
                return new Responce<Post>() 
                { 
                    Data = post
                    Description = "Post was saved"
                    StatusCode = 0 
                };
                return new Responce<Post>()
                { Data = null,
                    Description = "Post was not saved, please try again"
                  StatusCode = 54 
                };

            }

            public Responce<Post> ReadPost (int postId)
        {
                if (_context.Posts.Find(postId) != null)
                {
                    return new Response<Post>()
                    {
                        Data = _context.Posts.Find(postId),
                        Description = "Post Found",
                        StatusCode = 0
                    };
                }
                else
                {
                    return new Responce<Post>()
                    {
                        Data = null,
                        Description = "Post Not Found",
                        StatusCode = 50
                    };
                }

            }

            public Responce<Post> DeletePost (int postId)
            {
                var post = _context.Posts.Find(postId);
                if (post == null) return new Response<bool>() { Data = false, Description = "This post doesn't exist", StatusCode = 50 };

                _context.Posts.Remove(post);
                if (_context.SaveChanges() == 1)
                {
                    return new Response<bool>() { Data = true, Description = "Post Succesfully Deleted", StatusCode = 0 };
                }
                else
                {
                    return new Response<bool>() { Data = false, Description = "Could not save changes", StatusCode = 53 };
                }

            }

        public Responce<Post>UpdatePost(int postId, Post post)
        {
                var _contextPost = _context.Posts.Find(postId);
                if (_contextPost == null) throw new KeyNotFoundException();
                _contextPost.Text = post.Text;

                _context.SaveChanges();
                return new Responce<Post>()
                {
                    Data = _contextPost,
                    Description = "Post was Succesfully Updated",
                    StatusCode = 0
                };

            }
    }
}
