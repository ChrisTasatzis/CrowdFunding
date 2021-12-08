using CrowdFunding.DTO;
using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public interface IVideoService
    {
        Response<Video> CreateVideo(Video video, int projectId);
        Response<Video> ReadVideo(int videoId);
        Response<bool> DeleteVideo(int videoId);

  }
}
