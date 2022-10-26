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
    public class CommentController : Controller
    {
        ICommentService commentService;
        IRouteService routeService;
        public CommentController()
        {
            commentService = new CommentService();
            routeService = new RouteService();
        }

        //Get: Add Comment
        [Authorize]
        [HttpGet]
        public ActionResult AddCommentToRoute(int routeId)
        {
            return View();
        }

        //Post: Add Comment
        [HttpPost]
        public ActionResult AddCommentToRoute(Comment comment, int routeId)
        {
            {
                try
                {
                    commentService.AddCommentToRoute(comment, routeId);
                    return RedirectToAction("GetRoute", "Route", new { id = routeId });
                }
                catch
                {
                    return View();
                }
            }

        }
        //Get : Comment (To display details)
        public ActionResult GetComment(int id)
        {
            return View(commentService.GetComment(id));
        }

        //Get: Comments on Route page
        public ActionResult GetComments(int id)
        {
            return View(commentService.GetComments(id));
        }

        //Get: Edit Comment
        [Authorize]
        [HttpGet]
        public ActionResult EditComment(int id)
        {
            Comment comment = new Comment();
            //Getting comment
            comment = commentService.GetComment(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            if (!User.IsInRole("Admin") && comment.User != System.Web.HttpContext.Current.User.Identity.Name)
            {
                string errorMsg = "You must be the user who added this comment to edit it ";
                ViewBag.errorMsg = errorMsg;
                ViewBag.ID = comment.RouteId;
                return View();
            }
            //Displaying comment
            return View(comment);
        }
        //Post: Edit Comment
        [HttpPost]
        public ActionResult EditComment(Comment comment/*, int id*/)
        {
            {
                try
                {
                    commentService.EditComment(comment/*, id*/);
                    return RedirectToAction("GetRoute", "Route", new { ID = comment.RouteId });
                }

                catch
                {
                    return View();
                }
            }
        }

        //Get: Delete Comment
        [Authorize]
        [HttpGet]
        public ActionResult DeleteComment(int id)
        {
            Comment comment = new Comment();
            //Getting comment
            comment = commentService.GetComment(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            if (!User.IsInRole("Admin") && comment.User != System.Web.HttpContext.Current.User.Identity.Name)
            {
                string errorMsg = "You must be the user who added this comment to delete it ";
                ViewBag.errorMsg = errorMsg;
                ViewBag.ID = comment.RouteId;
                return View();
            }
            //Displaying comment
            return View(comment);
        }

        //Post: Delete Comment
        [HttpPost]
        public ActionResult DeleteComment(int routeId, int commentId)
        {
            {
                try
                {
                    //Deleting the comment
                    commentService.DeleteComment(commentId);
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
