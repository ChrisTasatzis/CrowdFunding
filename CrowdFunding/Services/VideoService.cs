using CrowdFunding.DTO;
using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public class VideoService : IVideoService
    {
        private readonly CFContext _context;
        public VideoService(CFContext context)
        {
            _context = context;
        }

        public Response<Video> CreateVideo(Video video, int projectId)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            if (projectId < 0)
            {
                return new Response<Video>()
                {
                    Data = null,
                    Description = "Project not found, please try again",
                    StatusCode = 50
                };
            }

            List<Video> videos = project.Videos;
            videos.Add(video);
            if (_context.SaveChanges() == 1)
                return new Response<Video>()
                {
                    Data = video,
                    Description = "Video was saved!",
                    StatusCode = 0
                };

            return new Response<Video>()
            {
                Data = null,
                Description = "Video was not saved, please try again",
                StatusCode = 54
            };

        }


        public Response<bool> DeleteVideo(int videoId)
        {
            var video = _context.Videos.FirstOrDefault(ph => ph.Id == videoId);
            if (video == null) return new Response<bool>() { Data = false, Description = "This video doesn't exist", StatusCode = 50 };

            _context.Videos.Remove(video);
            if (_context.SaveChanges() == 1)
            {
                return new Response<bool>() { Data = true, Description = "Video Succesfully Deleted", StatusCode = 0 };
            }
            else
            {
                return new Response<bool>() { Data = false, Description = "Could not save changes", StatusCode = 53 };

            }
        }

        public Response<Video> ReadVideo(int videoId)
        {
            var video = _context.Videos.FirstOrDefault(ph => ph.Id == videoId);
            if (video == null) return new Response<Video>() { Data = null, Description = "This video doesn't exist", StatusCode = 50 };

            return new Response<Video>()
            {
                Data = video,
                Description = "Video was saved!",
                StatusCode = 0
            };
        }

    }
}
    
