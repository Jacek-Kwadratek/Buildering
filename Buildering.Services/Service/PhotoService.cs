using Buildering.Data.DAO;
using Buildering.Data.IDAO;
using Buildering.Data.Models.Repository;
using Buildering.Services.IService;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Buildering.Services.Service
{
    public class PhotoService : IPhotoService
    {
        private IPhotoDAO photoDAO;

        public PhotoService()
        {
            photoDAO = new PhotoDAO();
        }

        public void AddPhotoToRoute(Photo photo, int id)
        {
            using (RouteDBContext context = new RouteDBContext())
            {
                string fileName = Path.GetFileNameWithoutExtension(photo.PhotoFile.FileName);
                string extension = Path.GetExtension(photo.PhotoFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                photo.Name = fileName;
                //Saving file path in DB
                photo.FilePath = "~/Buildering/ImageFolder/" + fileName;
                fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/ImageFolder/"), fileName);
                //Saving image 
                photo.PhotoFile.SaveAs(fileName);
                photoDAO.AddPhotoToRoute(photo, id, context);
                context.SaveChanges();
            }
        }

        public Photo GetPhoto(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                return photoDAO.GetPhoto(id, context);
            }
        }

        public IList<Photo> GetPhotos(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                return photoDAO.GetPhotos(id, context);
            }    
        }

        public void DeletePhoto(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                var photo = photoDAO.GetPhoto(id, context);
                photoDAO.DeletePhoto(photo, context);
                context.SaveChanges();
            }
        }
    }
}
