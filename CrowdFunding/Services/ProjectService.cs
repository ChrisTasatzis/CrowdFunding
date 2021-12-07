using CrowdFunding.DTO;
using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public class ProjectService : IProjectService
    {
        private readonly CFContext _db;

        public ProjectService(CFContext db)
        {
            _db = db;
        }
        public Response<bool> ActivateProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public Response<bool> AddFundingPackage(FundingPackage fundingPackage)
        {
            throw new NotImplementedException();
        }

        public Response<bool> AddMedia(Photo media)
        {
            throw new NotImplementedException();
        }

        public Response<bool> AddVideo(Video video, int projectId)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var media_ = video;
            if (media_ == null || project == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "Could not add video."
                };
            }
            else
            {
                project.Videos.Add(media_);
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 0,
                    Description = "Photo added succesfully."
                };
            }
        }
        public Response<bool> AddPost(Post post, int projectId)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var post_ = post;
            if (post_ == null || project == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "Could not add post."
                };
            }
            else
            {
                project.Posts.Add(post_);
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 0,
                    Description = "Post added succesfully."
                };
            }
        }

        public Response<bool> BackProject(int projectId, int userId, FundingPackage fundingPackage)
        {
            throw new NotImplementedException();
        }

        public Response<Project> CreateProject(Project project, int creatorId)
        {
            throw new NotImplementedException();
        }

        public Response<bool> DeactivateProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public Response<Project> ReadProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public Response<Project> ReadProject(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<Project>> ReadProject(int pageSize, int pageNumber)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 20) pageSize = 20;
            var projects = _db.Projects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new Response<List<Project>>
            {
                Data = projects,
                StatusCode = 0,
                Description = "OK."
            };
        }

        public Response<List<Project>> ReadProject(Category category, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Response<List<Project>> ReadProject(string name, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Response<bool> RemoveFundingPackage(FundingPackage fundingPackage, int projectId)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var fpackage_ = fundingPackage;
            if (fpackage_ == null || project == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "Could not remove the Funding Package."
                };
            }
            else
            {
                project.FundingPackages.Remove(fpackage_);
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 0,
                    Description = "Funding Package removed succesfully."
                };
            }
        }

        public Response<bool> RemoveMedia(Photo media)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var media_ = photo;
            if (media_ == null || project == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "Could not remove photo."
                };
            }
            else
            {
                project.Photos.Remove(media_);
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 0,
                    Description = "Photo removed succesfully."
                };
            }
        }

        public Response<bool> RemoveVideo(Video video, int projectId)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var media_ = video;
            if (media_ == null || project == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "Could not remove video."
                };
            }
            else
            {
                project.Videos.Remove(media_);
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 0,
                    Description = "Video removed succesfully."
                };
            }
        }
        public Response<bool> RemovePost(Post post, int projectId)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var media_ = post;
            if (media_ == null || project == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "Could not remove post."
                };
            }
            else
            {
                project.Posts.Remove(media_);
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 0,
                    Description = "Post removed succesfully."
                };
            }
        }

        public Response<Project> UpdateProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
