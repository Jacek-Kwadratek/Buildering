using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Services.IService
{
    public interface IPhotoService
    {
        void AddPhotoToRoute(Photo photo, int id);
        Photo GetPhoto(int id);
        IList<Photo> GetPhotos(int id);
        void DeletePhoto(int id);
    }
}
