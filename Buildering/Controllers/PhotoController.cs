using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using Buildering.Services.IService;
using Buildering.Services.Service;
using BuilderingApp.Models;

namespace Buildering.Controllers
{
    public class PhotoController : Controller
    {
        IPhotoService photoService;
        IRouteService routeService;
        public PhotoController()
        {
            photoService = new PhotoService();
            routeService = new RouteService();
        }
        
        //Get: Add Photo within Route
        [Authorize]
        [HttpGet]
        public ActionResult AddPhotoToRoute(int routeId)
        {
            return View();
        }
        //Post: Add Photo within Route
        [HttpPost]
        public ActionResult AddPhotoToRoute(Photo photo, int routeId)
        {
            try
            {
                photoService.AddPhotoToRoute(photo, routeId);
                return RedirectToAction("GetRoute", "Route", new { id = routeId });
            }
            catch
            {
                return View();
            }
        }

        //Get : Photo (To display details)
        public ActionResult GetPhoto(int id)
        {
            return View(photoService.GetPhoto(id));
        }

        //Get: Photos on Route page
        public ActionResult GetPhotos(int id)
        {
            Route route = new Route();
            route = routeService.GetRoute(id);
            IList<Photo> photo = route.Photos.ToList();
            return View(photo);
        }
        //Get: Delete Photo
        [Authorize]
        [HttpGet]
        public ActionResult DeletePhoto(int id)
        {
            Photo photo = new Photo();
            //Getting Photo
            photo = photoService.GetPhoto(id);
            //If photo not found
            if (photo == null)
            {
                return HttpNotFound();
            }
            if (!User.IsInRole("Admin") && photo.User != System.Web.HttpContext.Current.User.Identity.Name)
            {
                string errorMsg = "You must be the user who added this photo to delete it ";
                ViewBag.errorMsg = errorMsg;
                ViewBag.ID = photo.RouteId;
                return View();
            }
            //Displaying Photo
            return View(photo);
        }

        //Post: Delete Photo
        [HttpPost]
        public ActionResult DeletePhoto(int routeId, int photoId)
        {
            {
                try
                {
                    //Deleting the photo
                    photoService.DeletePhoto(photoId);
                    //Navigating back to the Route with the var routeID
                    return RedirectToAction("GetRoute", "Route", new { ID = routeId });
                }
                catch
                {
                    return View();
                }
            }
        }

    }
}
