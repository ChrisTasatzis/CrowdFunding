using CrowdFunding.DTO;
using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public interface IProjectService
    {
        Response<Project> CreateProject(Project project, int creatorId);
        Response<Project> ReadProject(int projectId);
        Response<Project> ReadProjectComplete(int projectId);
        Response<List<Project>> ReadProject(int pageSize, int pageNumber);
        Response<List<Project>> ReadProject(Category category, int pageSize, int pageNumber);
        Response<List<Project>> ReadProject(string name, int pageSize, int pageNumber);
        Response<Project> UpdateProject(Project project);
        Response<bool> DeleteProject(int projectId);
        Response<bool> ActivateProject(int projectId);
        Response<bool> DeactivateProject(int projectId);

        Response<bool> AddFundingPackage(FundingPackage fundingPackage, int projectId);
        Response<bool> RemoveFundingPackage(FundingPackage fundingPackage, int projectId);

        Response<bool> AddPhoto(Photo photo, int projectId);
        Response<bool> RemovePhoto(Photo photo, int projectId);

        Response<bool> AddVideo(Video video, int projectId);
        Response<bool> RemoveVideo(Video video, int projectId);

        Response<bool> AddPost(Post post, int projectId);
        Response<bool> RemovePost(Post post, int projectId);

        Response<List<Project>> ReadFeaturedProjects(int numOfProejcts, int duration);

        Response<List<BackerTotalProjectFunds>> ReadTopBakcers(int projectId, int numOfBackers);
        Response<bool> BackProject(int projectId, int userId, int fundingPackageId);

        Response<int> GetNumberOfPages(Category category, int pageSize);
        Response<int> GetNumberOfPages(int pageSize);
        Response<int> GetNumberOfPages(string name, int pageSize);
    }
}