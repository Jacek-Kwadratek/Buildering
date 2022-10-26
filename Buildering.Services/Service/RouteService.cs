using Buildering.Data.DAO;
using Buildering.Data.IDAO;
using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using Buildering.Services.IService;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Buildering.Services.Service
{
    public class RouteService : IRouteService
    {
        private IRouteDAO routeDAO;
        private IUserDAO userDAO;
        private ICommentDAO commentDAO;
        private IPhotoDAO photoDAO;
        private IVideoDAO videoDAO;

        public RouteService()
        {
            routeDAO = new RouteDAO();
            userDAO = new UserDAO();
            commentDAO = new CommentDAO();
            photoDAO = new PhotoDAO();
            videoDAO = new VideoDAO();
        }

        public void AddRoute(HttpPostedFileBase topo, Route route)
        {
            using (RouteDBContext context = new RouteDBContext())
            {
                routeDAO.AddRoute(topo, route, context);
                context.SaveChanges();
            }
            
        }

        public Route GetRoute(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                return routeDAO.GetRoute(id, context);
            }  
        }

        public IList<Route> GetRoutes()
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                return routeDAO.GetRoutes(context);
            }  
        }

        public IEnumerable<Route> SearchRoutes(Nullable<Buildering.Data.Models.Domain.Route.Grades> grading, string searchstring)
        {
            using(RouteDBContext context = new RouteDBContext())
            {

                //Calling GetRoutes function   
                var routes = from r in routeDAO.GetRoutes(context)
                             select r;

                //If no input, return all routes
                if (grading == null && string.IsNullOrEmpty(searchstring))
                {
                    return routes;
                }

                //If only name, find by name
                else if (grading == null && !string.IsNullOrEmpty(searchstring))
                {
                    routes = routes.Where(r => r.Name.ToLower().Contains(searchstring.ToLower()));
                }

                //If both or only grade, bring matching entries
                else
                {
                    routes = routes.Where(x => x.Grade == grading);
                    routes = routes.Where(r => r.Name.ToLower().Contains(searchstring.ToLower()));
                }
                //Displaying matching records
                return routes;
            }
        }

        public void EditRoute(HttpPostedFileBase topo, Route route/*, int id*/)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                routeDAO.EditRoute(topo, route, /*id,*/ context);
                context.SaveChanges();
            }     
        }

        public void DeleteRoute(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                    var route = routeDAO.GetRoute(id, context);

                    foreach (var comment in route.Comments.ToList())
                    {
                        commentDAO.DeleteComment(comment, context);
                    }

                    foreach (var photo in route.Photos.ToList())
                    {
                        photoDAO.DeletePhoto(photo, context);
                    }

                    foreach (var video in route.Videos.ToList())
                    {
                        videoDAO.DeleteVideo(video, context);
                    }

                    routeDAO.DeleteRoute(route, context);

                    context.SaveChanges();
            }
        }

        public void ContactAdmin(string name, string subject, string message, string returnUrl)
        {

            var senderEmail = new MailAddress("shu_sddp2021@outlook.com", "Saira");

            var receiverEmail = new MailAddress("saira_wasim@hotmail.com", "Admin");
            var password = "hello2021";
            var nme = name;
            var sub = subject;
            var body = message;
            var mess = new MailMessage();

            mess.To.Add(receiverEmail);
            mess.From = senderEmail;
            mess.Subject = "Message from: " + senderEmail;
            mess.Body = returnUrl + "\n" + "Name: " + name + "\n " + body;

            var smtp = new SmtpClient
            {
                Host = "smtp-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            smtp.Send(mess);
        }

        public bool VoteOnGrade(int routeId, int grade, string userEmail) 
        {
            using (RouteDBContext context = new RouteDBContext())
            {
                var user = userDAO.GetUserByEmail(userEmail, context);
                var route = routeDAO.GetRoute(routeId, context);
                if (!userDAO.CheckIfGraded(route, user))
                {
                    routeDAO.VoteOnGrade(context, route, grade);
                    userDAO.AddGradedRoute(route, user.UserId, context);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
