using Buildering.Data.DAO;
using Buildering.Data.IDAO;
using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using Buildering.Services.IService;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
namespace Buildering.Services.Service
{
    public class CommentService : ICommentService
    {
        private ICommentDAO commentDAO;
        private IRouteDAO routeDAO;

        public CommentService()
        {
            commentDAO = new CommentDAO();
            routeDAO = new RouteDAO();
        }

        public void AddCommentToRoute(Comment comment, int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                commentDAO.AddCommentToRoute(comment, id, context);
                context.SaveChanges();
            }
        }

        public Comment GetComment(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                return commentDAO.GetComment(id, context);
            }
            
        }

        public IList<Comment> GetComments(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                Route route = new Route();
                route = routeDAO.GetRoute(id, context);
                IList<Comment> comments = commentDAO.GetComments(route, context);
                return comments;
            }
        }

        public void EditComment(Comment comment)
        {
            using (RouteDBContext context = new RouteDBContext())
            {
                commentDAO.EditComment(comment, context);
                context.SaveChanges();
            }
            
        }

        public void DeleteComment(int id)
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                var comment = commentDAO.GetComment(id, context);
                commentDAO.DeleteComment(comment, context);
                context.SaveChanges();
            }
        }
    }
}
