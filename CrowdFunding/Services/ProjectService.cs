using CrowdFunding.DTO;
using CrowdFunding.Models;
using Microsoft.EntityFrameworkCore;
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
            {
                dbProject.isActive = true;
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

        public Response<bool> AddFundingPackage(FundingPackage fundingPackage, int projectId)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var fpackage_ = fundingPackage;
            if (fpackage_ == null || project == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "Could not add the Funding Package."
                };
            }
            else
            {
                project.FundingPackages.Add(fpackage_);
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 0,
                    Description = "Funding Package added succesfully."
                };
            }
        }

        public Response<bool> AddPhoto(Photo photo, int projectId)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var media_ = photo;
            if (media_ == null || project == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 10,
                    Description = "Could not add photo."
                };
            }
            else
            {
                project.Photos.Add(media_);
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 0,
                    Description = "Photo added succesfully."
                };
            }
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
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            var _fundingPackage = fundingPackage;

            if (_fundingPackage != null)
            {
                project.Progress += _fundingPackage.Price;
                project.Backers.Add(user);
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

        public Response<Project> CreateProject(Project project, int userId)
        {
            if (project == null)
            {
                return new Response<Project>()
                {
                    Data = null,
                    Description = " The project was null ",
                    StatusCode = 51
                };
            }

            if (_db.Projects.Where(p => p.Name == project.Name).Count() > 0)
                return new Response<Project>()
                {
                    Data = null,
                    Description = $"Project Name {project.Name} is already taken.",
                    StatusCode = 52
                };

            _db.Projects.Add(project);

            var creator = _db.Users.FirstOrDefault(u => u.Id == userId);

            project.ProjectCreator = creator;
            if (_db.SaveChanges() == 1)
            {
                return new Response<Project>()
                {
                    Data = project,
                    Description = "Project succesfully created",
                    StatusCode = 0
                };
            }

            return new Response<Project>()
            {
                Data = null,
                Description = "Project creation was unsuccesfull",
                StatusCode = 50
            };
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
            {
                _db.Projects.Remove(dbProject);
                _db.SaveChanges();
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 11,
                    Description = "Project deleted succefully."
                };

            }
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
                StatusCode = 0,
                Description = "OK."
            };
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
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 20) pageSize = 20;

            List<Project> projects =
            _db.Projects
                .Where(project => project.Category == category)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (_db.Projects.Count() > 0)
            {
                foreach (var project in _db.Projects)
                    return new Response<List<Project>>
                    {
                        Data = projects,
                        StatusCode = 19,
                        Description = "The projects were succesfully read"
                    };
            }
            else
                return new Response<List<Project>>
                {
                    Data = null,
                    StatusCode = 33,
                    Description = "The projects were not succesfully read"
                };

            return new Response<List<Project>>
            {
                Data = null,
                StatusCode = 33,
                Description = "The projects were not succesfully read"
            };

        }

        public Response<List<Project>> ReadProject(string name, int pageSize, int pageNumber)
        {
            //    var project = _db.Projects.FirstOrDefault(p => p.Name == name);

            //    if (project == null)
            //        return new Response<List<Project>>
            //        {
            //            Data = null,
            //            StatusCode = 10,
            //            Description = "No project with this id exists."
            //        };

            //    return new Response<List<Project>>
            //    {
            //        Data = projects,
            //        StatusCode = 12,
            //        Description = "Project Found."
            //    };
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 20) pageSize = 20;

            List<Project> projects =
            _db.Projects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (_db.Projects.Count() > 0)
            {
                foreach (var project in _db.Projects)
                    return new Response<List<Project>>
                    {
                        Data = projects,
                        StatusCode = 19,
                        Description = "The projects were succesfully read"
                    };
            }
            else
                return new Response<List<Project>>
                {
                    Data = null,
                    StatusCode = 33,
                    Description = "The projects were not succesfully read"
                };

            return new Response<List<Project>>
            {
                Data = null,
                StatusCode = 12,
                Description = "Project Found."
            };
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

        public Response<bool> RemovePhoto(Photo photo, int projectId)
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

        public Response<List<Project>> ReadFeaturedProjects(int numOfProejcts, int duration) // not working yet
        {

            var featured = _db.Set<ProjectBacker>()
                .Where(pb => (DateTime.Now - pb.DateTime).TotalDays < duration)
                .GroupBy(p => p.ProjectId)
                .Select(cl => new
                {
                    projectId = cl.First().ProjectId,
                    backing = cl.Sum(c => c.FundingPackage.Price)
                })
                .OrderBy(p => p.backing)
                .Take(numOfProejcts)
                .Select(p => _db.Projects.First(proj => proj.Id == p.projectId))
                .ToList();

            return new Response<List<Project>>
            {
                Data = featured,
                StatusCode = 0,
                Description = "OK."
            };
        }

        public Response<Project> ReadProjectComplete(int projectId)
        {
            var project = _db.Projects.Where(p => p.Id == projectId)
                .Include(p => p.ProjectCreator)
                .Include(p => p.Posts)
                .Include(p => p.FundingPackages)
                .Include(p => p.Photos)
                .Include(p => p.Videos)
                .FirstOrDefault();

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
                StatusCode = 0,
                Description = "OK."
            };
        }

        public Response<List<BackerTotalProjectFunds>> ReadTopBakcers(int projectId, int numOfBackers)
        {
            var backerFunds = _db.Set<ProjectBacker>()
                .Where(pb => pb.ProjectId == projectId)
                .Include(pb => pb.FundingPackage)
                .GroupBy(pb => pb.BackerId)
                .Select(cl => new BackerIdTotalProjectFunds()
                {
                    BackerId = cl.First().BackerId,
                    TotalFunds = cl.Sum(c => c.FundingPackage.Price)
                })
                .OrderByDescending(cl => cl.TotalFunds)
                .Take(numOfBackers)
                .ToList();

            var backers = new List<BackerTotalProjectFunds>();

            foreach (var backerFund in backerFunds)
            {
                var user = _db.Users.Find(backerFund.BackerId);
                backers.Add(new BackerTotalProjectFunds()
                {
                    Backer = user,
                    TotalFunds = backerFund.TotalFunds
                });
            }

            return new Response<List<BackerTotalProjectFunds>>
            {
                Data = backers,
                StatusCode = 0,
                Description = "OK."
            };
        }

        public Response<bool> BackProject(int projectId, int userId, int fundingPackageId)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == projectId);
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            var fundingPackage = _db.FundingPackages.FirstOrDefault(f => f.Id == fundingPackageId);

            _db.Set<ProjectBacker>().Add(new ProjectBacker
            {
                ProjectId = projectId,
                BackerId = userId,
                FundingPackage = fundingPackage
            });

            if (_db.SaveChanges() != 1)
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 53,
                    Description = "Could not save changes."
                };

            return new Response<bool>
            {
                Data = true,
                StatusCode = 0,
                Description = "User successfully backed the project."
            };
        }
    }
}
