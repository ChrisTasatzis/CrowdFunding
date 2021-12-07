using CrowdFunding.DTO;
using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public interface IPhotoService
    {
        Response<Photo> CreatePhoto(Photo photo, int projectId);
        Response<Photo> GetPhoto(int photoId);
        Response<bool> DeletePhoto(int photoId);

  }
}
