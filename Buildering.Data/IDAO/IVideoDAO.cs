using Buildering.Data.Models.Repository;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Data.IDAO
{
    public interface IVideoDAO
    {
        void AddVideoToRoute(Video video, int id, RouteDBContext context);
        Video GetVideo(int id, RouteDBContext context);
        IList<Video> GetVideos(int id, RouteDBContext context);
        void DeleteVideo(Video video, RouteDBContext context);
    }
}
