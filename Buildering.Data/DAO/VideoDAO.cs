using Buildering.Data.IDAO;
using Buildering.Data.Models.Repository;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Data.DAO
{
    public class VideoDAO : IVideoDAO
    {

        public void AddVideoToRoute(Video video, int id, RouteDBContext context)
        {
            //adding id to video
            video.RouteId = id;
            context.Videos.Add(video);
        }

        public Video GetVideo(int id, RouteDBContext context)
        {
            return context.Videos.Find(id);
        }

        public IList<Video> GetVideos(int id, RouteDBContext context)
        {
            return context.Videos.ToList();
        }

        public void DeleteVideo(Video video, RouteDBContext context)
        {
            context.Videos.Remove(video);
        }

    }
}
