using Buildering.Data.DAO;
using Buildering.Data.IDAO;
using Buildering.Data.Models.Repository;
using Buildering.Services.IService;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Services.Service
{
    public class VideoService : IVideoService
    {
        private IVideoDAO videoDAO;

        public VideoService()
        {
            videoDAO = new VideoDAO();
        }

        public void AddVideoToRoute(Video video, int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                videoDAO.AddVideoToRoute(video, id, context);
                context.SaveChanges();
            }
        }

        public Video GetVideo(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                return videoDAO.GetVideo(id, context);
            }
        }

        public IList<Video> GetVideos(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                return videoDAO.GetVideos(id, context);
            }    
        }

        public void DeleteVideo(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                var video = videoDAO.GetVideo(id, context);
                videoDAO.DeleteVideo(video, context);
                context.SaveChanges();
            }
        }
    }
}
