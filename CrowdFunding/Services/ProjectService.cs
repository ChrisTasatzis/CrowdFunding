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
            var dbProject = _db.Projects.Find(projectId);
            if (dbProject == null)
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "No Project with this id exists"

                };
            if (dbProject.isActive == true)
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 13,
                    Description = "The Project is already active."
                };
            if (dbProject.isActive != true)
            {   dbProject.isActive = true; 
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 14,
                    Description = "The Project has been activated."
                };
            }
            return new Response<bool>
            {
                Data = false,
                StatusCode = 15,
                Description = "the project could not be activated"

            };
        }

        public Response<bool> AddFundingPackage(FundingPackage fundingPackage, int projectId)//mhpws thelei kai to id gia na mpei to fpackage sto project me to sygkekrimeno id?/epishs xreiazetai auto to function afou exoyme to backproject?
        {
            throw new NotImplementedException();
           // var project = 
        }

        public Response<bool> AddMedia(Photo media)//den exw idea pws kanw add to media sto media list tou project//epishs den volevei na exoume 2 diaforetika functions gia photo kai video?
        {
            var media_ = media;
            if (media_ == )
            {
                
            }
        }

        public Response<bool> AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        public Response<bool> BackProject(int projectId, int userId, FundingPackage fundingPackage)// na pernaei o backer sto project backers
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            var _fundingPackage = fundingPackage;

            if (_fundingPackage != null)
            {
                project.Progress += _fundingPackage.Price;
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 0,
                    Description = "User successfully backed the project."
                };
            }
            else
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 0,
                    Description = "The project could not be backed."
                };
        }

        public Response<Project> CreateProject(Project project, int userId)//ti paizei me creator id,den yparei pouthena allou
        {
            throw new NotImplementedException();
        }

        public Response<bool> DeactivateProject(int projectId)
        {
            var dbProject = _db.Projects.Find(projectId);
            if (dbProject == null)
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "No Project with this id exists"

                };
            if (dbProject.isActive != true)
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 13,
                    Description = "The Project is not active."
                };
            if (dbProject.isActive == true)
            {
                dbProject.isActive = false;
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 14,
                    Description = "The Project has been deactivated."
                };
            }
            return new Response<bool>
            {
                Data = false,
                StatusCode = 15,
                Description = "the project could not be deactivated"
            };
        }
        public Response<bool> DeleteProject(int projectId)
        {
            var dbProject = _db.Projects.Find(projectId);
            if (dbProject == null)
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "No Project with this id exists."
               };
            else
                _db.Projects.Remove(dbProject);
                _db.SaveChanges();
            return new Response<bool>
            {
                Data = true,
                StatusCode = 11,
                Description = "Project deleted succefully."
            };

        }

        public Response<Project> ReadProject(int projectId)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project == null)
                return new Response<Project>
                {
                    Data = null,
                    StatusCode = 10,
                    Description = "No project with this id exists."
                };

            return new Response<Project>
            {
                Data = project,
                StatusCode = 12,
                Description = "Project Found."
            };
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
            var project = _db.Projects.FirstOrDefault(p => p.Name == name);

            if (project == null)
                return new Response<List<Project>>
                {
                    Data = null,
                    StatusCode = 10,
                    Description = "No project with this id exists."
                };

            return new Response<List<Project>>
            {
                Data = project,
                StatusCode = 12,
                Description = "Project Found."
            };
        }

        public Response<bool> RemoveFundingPackage(FundingPackage fundingPackage)
        {
            throw new NotImplementedException();
        }

        public Response<bool> RemoveMedia(Photo media,int  projectId)
        {
            throw new NotImplementedException();
        }

        public Response<bool> RemovePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Response<Project> UpdateProject(Project project)//mikro thema me ta null edw epeidh xreiazetai kapoio project gia return, epishs kapoia fiels den einai nullable
        {
            if (project == null)
                return new Response<Project>
                {
                    Data = null,
                    StatusCode = 16,
                    Description = "No valid project given."
                };

            if (project.Id == 0)
                return new Response<Project>
                {
                    Data = null,
                    StatusCode = 10,
                    Description = "No project with this id exists."
                };

            var projectdb = _db.Projects.FirstOrDefault(p => p.Id == project.Id);

            if (projectdb == null)
                return new Response<Project>
                {
                    Data = null,
                    StatusCode = 10,
                    Description = "No project with this id exists."
                };

            if (project.Id != null) projectdb.Id = project.Id;
            if (project.Name != null) projectdb.Name = project.Name;
            if (project.Description != null) projectdb.Description = project.Description;
            if (project.Category != null) projectdb.Category = project.Category;
            if (project.Goal != null) projectdb.Goal = project.Goal;
            if (project.Progress != null) projectdb.Progress = project.Progress;
            if (project.isActive != null) projectdb.isActive = project.isActive;
            if (project.ProjectCreator != null) projectdb.ProjectCreator = project.ProjectCreator;
            if (project.Backers != null) projectdb.Backers = project.Backers;
            if (project.Posts != null) projectdb.Posts = project.Posts;
            if (project.FundingPackages != null) projectdb.FundingPackages = project.FundingPackages;
            if (project.Photos != null) projectdb.Photos = project.Photos;
            if (project.Videos != null) projectdb.Videos = project.Videos;

            if (_db.SaveChanges() != 1)
                return new Response<Project>
                {
                    Data = null,
                    StatusCode = 17,
                    Description = "Could not save changes."
                };

            return new Response<Project>
            {
                Data = projectdb,
                StatusCode = 18,
                Description = "Project was successfully updated."
            };
        }
    }
}
