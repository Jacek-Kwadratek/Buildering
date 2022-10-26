using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class VideoController : Controller
    {
        IVideoService videoService;
        IRouteService routeService;

        public VideoController()
        {
            videoService = new VideoService();
            routeService = new RouteService();
        }

        //Get: Add Video within Route
        [Authorize]
        [HttpGet]
        public ActionResult AddVideoToRoute(int routeId)
        {
            return View();
        }
        //Post: Add Video within Route
        [HttpPost]
        public ActionResult AddVideoToRoute(Video video, int routeId)
        {
            {
                try
                {
                    videoService.AddVideoToRoute(video, routeId);
                    return RedirectToAction("GetRoute", "Route", new { id = routeId });
                }
                catch
                {
                    return View();
                }
            }

        }
        //Get : Video (To display details)
        public ActionResult GetVideo(int id)
        {
            return View(videoService.GetVideo(id));
        }

        //Get: Videos on Route page
        public ActionResult GetVideos(int id)
        {
            Route route = new Route();
            route = routeService.GetRoute(id);
            IList<Video> video = route.Videos.ToList();
            return View(video);
        }

        //Get: Delete Video
        [Authorize]
        [HttpGet]
        public ActionResult DeleteVideo(int id)
        {
            Video video = new Video();
            //Getting Video
            video = videoService.GetVideo(id);
            //If video not found
            if (video == null)
            {
                return HttpNotFound();
            }
            if (!User.IsInRole("Admin") && video.User != System.Web.HttpContext.Current.User.Identity.Name)
            {
                string errorMsg = "You must be the user who added this comment to edit it ";
                ViewBag.errorMsg = errorMsg;
                ViewBag.ID = video.RouteId;
                return View();
            }
            //Displaying video
            return View(video);
        }

        //Post: Delete Video
        [HttpPost]
        public ActionResult DeleteVideo(int routeId, int videoId)
        {
            {
                try
                {
                    //Deleting the video
                    videoService.DeleteVideo(videoId);
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
