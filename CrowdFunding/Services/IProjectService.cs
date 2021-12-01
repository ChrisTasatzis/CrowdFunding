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
        Response<Project> ReadProject(int id);
        Response<List<Project>> ReadProject(int pageSize, int pageNumber);
        Response<List<Project>> ReadProject(Category category, int pageSize, int pageNumber);
        Response<List<Project>> ReadProject(string name, int pageSize, int pageNumber);
        Response<Project> UpdateProject(Project project);
        Response<bool> DeleteProject(Project project);
        Response<bool> ActivateProject(int projectId);
        Response<bool> DeactivateProject(int projectId);

        Response<bool> AddFundingPackage(FundingPackage fundingPackage);
        Response<bool> RemoveFundingPackage(FundingPackage fundingPackage);

        Response<bool> AddMedia(Photo media);
        Response<bool> RemoveMedia(Photo media);

        Response<bool> AddPost(Post post);
        Response<bool> RemovePost(Post post);


    }
}
