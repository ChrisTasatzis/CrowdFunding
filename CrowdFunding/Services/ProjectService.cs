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
        public Response<bool> ActivateProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public Response<bool> AddFundingPackage(FundingPackage fundingPackage)
        {
            throw new NotImplementedException();
        }

        public Response<bool> AddMedia(Media media)
        {
            throw new NotImplementedException();
        }

        public Response<bool> AddPost(Post post)
        {
            throw new NotImplementedException();
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

        public Response<bool> DeleteProject(Project project)
        {
            throw new NotImplementedException();
        }

        public Response<Project> ReadProject(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<Project>> ReadProject(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Response<List<Project>> ReadProject(Category category, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Response<List<Project>> ReadProject(string name, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Response<bool> RemoveFundingPackage(FundingPackage fundingPackage)
        {
            throw new NotImplementedException();
        }

        public Response<bool> RemoveMedia(Media media)
        {
            throw new NotImplementedException();
        }

        public Response<bool> RemovePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Response<Project> UpdateProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
