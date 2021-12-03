using CrowdFunding.DTO;
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
        Response<Post> CreatePost(Post post, int projectId);
        Response<Post> ReadPost(int postid);
        Response<bool> DeletePost(int postid);
        Response<Post> UpdatePost(int postid, Post post );
    }
}
