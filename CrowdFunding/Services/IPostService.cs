using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public interface IPostService
    {
        public Post CreatePost(Post post, projectId);
        public Post ReadPost(int id);
        public List<Post> ReadPost(int pageCount,int pageSize);
        public bool DeletePost(int id);
        public Post UpdatePost(int id, Post post );
    }
}
