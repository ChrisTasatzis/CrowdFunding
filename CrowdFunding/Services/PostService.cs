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
        public Response<Post> CreatePost(Post post, int projectId)

        {
            var project = _context.Projects.Find(projectId);
            if (projectId < 0)
            {
                return new Response<Post>()
                {
                    Data = null,
                    Description = "Project not found, please try again",
                    StatusCode = 50
                };
            }

            List<Post> posts = project.Posts;
            posts.Add(post);
            if (_context.SaveChanges() == 1) ;
            return new Response<Post>()
            {
                Data = post,
                Description = "Post was saved!",
                StatusCode = 0
            };
            return new Response<Post>()
            {
                Data = null,
                Description = "Post was not saved, please try again",
                StatusCode = 54
            };

        }


        public Response<bool> DeletePost(int postId)
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

        public Response<Post> ReadPost(int postId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                return new Response<Post>()
                {
                    Data = post,
                    Description = "Post Found",
                    StatusCode = 0
                };
            }
            else
            {
                return new Response<Post>()
                {
                    Data = null,
                    Description = "Post Not Found",
                    StatusCode = 50
                };
            }

        }

        public List<Post> ReadAllPosts(int projectId, int pageSize, int pageNumber)
        {
            var project = _context.Projects.Find(projectId);
            if (project == null) throw new KeyNotFoundException();
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 20) pageSize = 20;
            return project.Posts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        }


        public Response<Post> UpdatePost(int postId, Post post)
        {
            var _contextPost = _context.Posts.Find(postId);
            if (_contextPost == null) throw new KeyNotFoundException();
            _contextPost.Text = post.Text;

            _context.SaveChanges();
            return new Response<Post>()
            {
                Data = _contextPost,
                Description = "Post was Succesfully Updated",
                StatusCode = 0
            };

        }
    }
    }

