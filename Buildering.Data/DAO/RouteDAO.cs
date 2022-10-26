using Buildering.Data.IDAO;
using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Web;
using System.Net;
using System.Data.Entity;
using System.Net.Mail;

namespace Buildering.Data.DAO
{
    public class RouteDAO : IRouteDAO
    {

        public void AddRoute(HttpPostedFileBase topo, Route route, RouteDBContext context)
        {
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/ImageFolder/"), Path.GetFileName(topo.FileName));
                topo.SaveAs(path);
                    route = new Route()
                    {
                        Name = route.Name,
                        Coordinate = route.Coordinate,
                        Description = route.Description,
                        //Storing path with the 'path' variable 
                        Topo = path,
                        //Storing Filename with HttpPostedFileBase method
                        TopoName = topo.FileName,
                        Rating = route.Rating,
                        Grade = route.Grade,
                        User = route.User
                    };
            context.Routes.Add(route);     
        }

        public Route GetRoute(int id, RouteDBContext context)
        {
            //Eager loading required so Route view initalizes with the partial views for comments, photos and videos
            context.Routes.Include(r => r.Comments).ToList();
            context.Routes.Include(r => r.Photos).ToList();
            context.Routes.Include(r => r.Videos).ToList(); 
            return context.Routes.Find(id);
        }

        public IList<Route> GetRoutes(RouteDBContext context)
        {
            return context.Routes.ToList();
        }

        public void EditRoute(HttpPostedFileBase topo, Route route, RouteDBContext context)
        {
            //Attaching the modified entry to context
            if (route.Topo != null)
            {
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/ImageFolder/"), Path.GetFileName(topo.FileName));
                topo.SaveAs(path);
                route.Topo = path;
                route.TopoName = topo.FileName;
            }
            context.Entry(route).State = EntityState.Modified;
        }

        public void DeleteRoute(Route route, RouteDBContext context)
        {
            context.Routes.Remove(route);
        }

        public void VoteOnGrade(RouteDBContext context, Route route, int grade)
        {
            route.VotedGradeNum = ((route.VotedGradeNum * route.VotedGradeCount) + grade) / (route.VotedGradeCount + 1);
            route.VotedGradeCount++;
            route.VotedGrade = (Models.Domain.Route.Grades)Math.Round(route.VotedGradeNum, 0);
        }
    }
}


