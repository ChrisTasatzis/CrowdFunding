using CrowdFunding.DTO;
using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly CFContext _context;
        public PhotoService(CFContext context)
        {
            _context = context;
        }

        public Response<Photo> CreatePhoto(Photo photo, int projectId)
        {
            var project = _context.Projects.Find(projectId);
            if (projectId < 0)
            {
                return new Response<Photo>()
                {
                    Data = null,
                    Description = "Project not found, please try again",
                    StatusCode = 50
                };
            }

            List<Photo> photos = project.Photos;
            photos.Add(photo);
            if (_context.SaveChanges() == 1) ;
            return new Response<Photo>()
            {
                Data = photo,
                Description = "Photo was saved!",
                StatusCode = 0
            };
            return new Response<Photo>()
            {
                Data = null,
                Description = "Photo was not saved, please try again",
                StatusCode = 54
            };

        }


        public Response<bool> DeletePhoto(int photoId)
        {
            var photo = _context.Photos.Find(photoId);
            if (photo == null) return new Response<bool>() { Data = false, Description = "This photo doesn't exist", StatusCode = 50 };

            _context.Photos.Remove(photo);
            if (_context.SaveChanges() == 1)
            {
                return new Response<bool>() { Data = true, Description = "Photo Succesfully Deleted", StatusCode = 0 };
            }
            else
            {
                return new Response<bool>() { Data = false, Description = "Could not save changes", StatusCode = 53 };

            }
        }

        public Response<Photo> GetPhoto(int photoId)
        {
            var photo = _context.Photos.Find(photoId);
            if (photo == null) return new Response<Photo>() { Data = null, Description = "This photo doesn't exist", StatusCode = 50 };

            return new Response<Photo>()
            {
                Data = photo,
                Description = "Photo was saved!",
                StatusCode = 0
            };
        }

    }
}
    
