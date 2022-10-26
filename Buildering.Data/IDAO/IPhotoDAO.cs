using Buildering.Data.Models.Repository;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Data.IDAO
{
    public interface IPhotoDAO
    {
        void AddPhotoToRoute(Photo photo, int id, RouteDBContext context);
        Photo GetPhoto(int id, RouteDBContext context);
        IList<Photo> GetPhotos(int id, RouteDBContext context);
        void DeletePhoto(Photo photo, RouteDBContext context);
    }
}
