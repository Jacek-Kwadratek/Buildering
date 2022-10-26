using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Services.IService
{
    public interface IVideoService
    {
        void AddVideoToRoute(Video video, int id);
        Video GetVideo(int id);
        IList<Video> GetVideos(int id);
        void DeleteVideo(int id);
    }
}
