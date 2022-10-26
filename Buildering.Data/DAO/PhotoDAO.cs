using Buildering.Data.IDAO;
using Buildering.Data.Models.Repository;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buildering.Data.DAO
{
    public class PhotoDAO : IPhotoDAO
    {
        public void AddPhotoToRoute(Photo photo, int id, RouteDBContext context)
        {
                photo = new Photo()
                {
                    PhotoId = photo.PhotoId,
                    Title = photo.Title,
                    FilePath = photo.FilePath,
                    Name = photo.Name,
                    RouteId = id,
                    User = photo.User
                };
           
            context.Photos.Add(photo);
        }

        public Photo GetPhoto(int id, RouteDBContext context)
        {
            return context.Photos.Find(id);
        }

        public IList<Photo> GetPhotos(int id, RouteDBContext context)
        {
            return context.Photos.ToList();
        }

        public void DeletePhoto(Photo photo, RouteDBContext context)
        {
            context.Photos.Remove(photo);
            string path = HttpContext.Current.Server.MapPath("~/ImageFolder/") + photo.Name;
            File.Delete(@path);
        }
    }
}
