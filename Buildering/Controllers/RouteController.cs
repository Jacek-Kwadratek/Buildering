using Buildering.Data.Models.Domain;
using Buildering.Services.IService;
using Buildering.Services.Service;
using BuilderingApp.Models;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;//for contact action
using System.Net.Mail; // for navigating email
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Buildering.Controllers
{
    public class RouteController : Controller
    {
        IRouteService routeService;
        IUserService userService;
        public RouteController()
        {
            routeService = new RouteService();
            userService = new UserService();
        }
        //Get: Contact Admin Action
        [Authorize]
        [HttpGet]
        public ActionResult ContactAdmin(string returnUrl)
        {
            returnUrl = Request.UrlReferrer.AbsoluteUri;
            ViewData["URL"] = returnUrl;
            return View();
        }

        //Post: Contact Admin Action
        [HttpPost]
        public ActionResult ContactAdmin(string name, string subject, string message, 
            string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    routeService.ContactAdmin(name, subject, message, returnUrl);
                    return RedirectToAction("GetRoutes", "Route");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }

        // GET: Route/Create
        [Authorize]
        [HttpGet]
        public ActionResult AddRoute(string Coordinate)
        {

            ViewBag.Coordinate = Coordinate;
            return View();

        }
        // POST: Route/Create
        [HttpPost]
        public ActionResult AddRoute(HttpPostedFileBase topo, Route route)
        {
            try
            {
                routeService.AddRoute(topo, route);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // Get: Route
        public ActionResult GetRoute(int id)
        {
            return View(routeService.GetRoute(id));
        }

        public ActionResult GetRoutes(Nullable<Buildering.Data.Models.Domain.Route.Grades> grading, string searchstring)
        {
            return View(routeService.SearchRoutes(grading, searchstring));
        }

        //Get: Edit Route
        public ActionResult EditRoute(int id)
        {
            Route route = new Route();
            route = routeService.GetRoute(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            if (!User.IsInRole("Admin") && route.User != System.Web.HttpContext.Current.User.Identity.Name)
            {
                string errorMsg = "You must be the user who created this route to edit it ";
                ViewBag.errorMsg = errorMsg;
                ViewBag.ID = id;
                return View(); 
            }
            return View(route);
        }
        //Post: Edit Route
        [HttpPost]
        public ActionResult EditRoute(HttpPostedFileBase topo, Route route/*, int id*/)
        {
            {
                try
                {
                    routeService.EditRoute(topo, route/*, id*/);
                    return RedirectToAction("GetRoute", "Route", new { id = route.ID });
                }

                catch
                {
                    return View();
                }
            }
        }
        //Get : Delete Route
        [Authorize]
        [HttpGet]
        public ActionResult DeleteRoute(int id)
        {
            Route route = new Route();
            //Getting route
            route = routeService.GetRoute(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            //Checks if user is an admin or the original uploader
            if (!User.IsInRole("Admin") && route.User != System.Web.HttpContext.Current.User.Identity.Name)
            {
                string errorMsg = "You must be the user who created this route to edit it ";
                ViewBag.errorMsg = errorMsg;
                ViewBag.ID = id;
                return View();
            }
            return View(route);
        }

        //Post: Delete Route
        [HttpPost]
        public ActionResult DeleteRoute(Route route, int id)
        {
            {
                try
                {
                    //Deleting route
                    routeService.DeleteRoute(id);
                    return RedirectToAction("GetRoutes", "Route");
                }
                catch
                {
                    return View();
                }
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult VoteOnGrade(int ID)
        {
            return View(); // change to redirect to action 
        }

        [HttpPost]
        public ActionResult VoteOnGrade(int ID, Buildering.Data.Models.Domain.Route.Grades VotedGrade, string userName)
        {
            var result = routeService.VoteOnGrade(ID, (int)VotedGrade, userName);
            if (result)
            {
                return RedirectToAction("GetRoute", "Route", new { id = ID });
            }
            else
            {
                string errorMsg = "You have already voted on this route's grade";
                ViewBag.errorMsg = errorMsg;
                ViewBag.ID = ID;
                return View();
            }
        }
    }
}