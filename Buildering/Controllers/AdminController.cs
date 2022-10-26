using Buildering.Models;
using Buildering.Services.IService;
using Buildering.Services.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buildering.Controllers
{
    public class AdminController : Controller
    {
        IRouteService routeService;
        IUserService userService;
        ApplicationDbContext context; 
        public AdminController()
        {
            context = new ApplicationDbContext();
            userService = new UserService();
            routeService = new RouteService();
        }

       [Authorize(Roles ="Admin")]
        public ActionResult GetUsers()
        {
            return View(userService.GetUsers());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetRoles()
        {
            return View(context.Roles.ToList());
        }

        // GET: Admin/AddRole
        [Authorize(Roles = "Admin")]
        public ActionResult AddRole()
        {
            return View();
        }
                
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddRole(FormCollection collection)
        {
            IdentityRole role = new IdentityRole(collection["RoleName"]);
            context.Roles.Add(role);
            context.SaveChanges();
            return RedirectToAction("GetRoles");
        }
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {     
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: Admin/DeleteRole/xxx
        public ActionResult DeleteRole(string roleId)
        {
            try
            {
                var roles = context.Roles.ToList();
                var role = roles.Where(d => d.Id == roleId).FirstOrDefault();
                context.Roles.Remove(role);
                context.SaveChanges();
                return View(ViewBag.Result = true);
            }
            catch
            {
                return View(ViewBag.Result = false);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddUserToRole()
        {
            var userList = context.Users.OrderBy(
                u => u.UserName).ToList().Select(
                uu => new SelectListItem {
                    Value = uu.UserName.ToString(),
                    Text = uu.UserName }).ToList();
            ViewBag.Users = userList;

            var roleList = context.Roles.OrderBy(
                r => r.Name).ToList().Select
                (rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name}).ToList();
            ViewBag.Roles = roleList;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddUserToRole(string userName, string roleName)
        {
            ApplicationUser user = // Create the ApplicationUser object
                context.Users.Where
                (u => u.UserName.Equals(userName,
                StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var um = new UserManager<ApplicationUser>   // Create the UserManager object
                (new UserStore<ApplicationUser>(context));
            var idResult = um.AddToRole(user.Id, roleName); // Use UserManager object to add user to role

            // Go back to where you were
            // Populate roles for the view DropDown
            var roleList = context.Roles.OrderBy
                (r => r.Name).ToList().Select
                (rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;
            // prepopulate users for the view DropDown
            var userList = context.Users.OrderBy
                (u => u.UserName).ToList().Select
                (uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userList;
            return View("AddUserToRole");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveUserFromRole()
        {
            var userList = context.Users.OrderBy(
                u => u.UserName).ToList().Select(
                uu => new SelectListItem
                {
                    Value = uu.UserName.ToString(),
                    Text = uu.UserName
                }).ToList();
            ViewBag.Users = userList;

            var roleList = context.Roles.OrderBy(
                r => r.Name).ToList().Select
                (rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RemoveUserFromRole(string userName, string roleName)
        {
            ApplicationUser user = // Create the ApplicationUser object
                context.Users.Where
                (u => u.UserName.Equals(userName,
                StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var um = new UserManager<ApplicationUser>   // Create the UserManager object
                (new UserStore<ApplicationUser>(context));
            var idResult = um.RemoveFromRole(user.Id, roleName); // Use UserManager object to add user to role

            // Go back to where you were
            // Populate roles for the view DropDown
            var roleList = context.Roles.OrderBy
                (r => r.Name).ToList().Select
                (rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;
            // prepopulate users for the view DropDown
            var userList = context.Users.OrderBy
                (u => u.UserName).ToList().Select
                (uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userList;
            return View("RemoveUserFromRole");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetRolesForUser()
        {
            var userList = context.Users.OrderBy(u => u.UserName).ToList().Select(
                uu => new SelectListItem
                {
                    Value = uu.UserName.ToString(),
                    Text = uu.UserName
                }).ToList();
            ViewBag.Users = userList;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult GetRolesForUser(string username)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals
            (username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ViewBag.userRoles = um.GetRoles(user.Id);
            return View("GetRolesForUserConfirmed");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetRoutes()
        {
            return View(routeService.GetRoutes());
        }
    }
}
