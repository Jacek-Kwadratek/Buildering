using Buildering.Data.Models.Domain;
using Buildering.Services.IService;
using Buildering.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Buildering.Controllers
{
    public class HomeController : Controller
    {
        IRouteService routeService;
        public HomeController()
        {
            routeService = new RouteService();
        }

        public ActionResult Index()
        {
            ViewBag.Routes = routeService.GetRoutes();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
