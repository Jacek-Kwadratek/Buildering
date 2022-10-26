using Buildering.Services.IService;
using Buildering.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buildering.Controllers
{
    public class UserController : Controller
    {
        IUserService userService;
        public UserController()
        {
            userService = new UserService();
        }

        public ActionResult GetUsers()
        {
            return View(userService.GetUsers());
        }

        public ActionResult GetUserByEmail(string email)
        {
            return View(userService.GetUserByEmail(email));
        }

        public ActionResult DeleteUser(string email)
        {
            userService.DeleteUser(email);
            return RedirectToAction("GetUsers");
        }
    }
}
