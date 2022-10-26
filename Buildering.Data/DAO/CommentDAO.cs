using Buildering.Data.IDAO;
using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using BuilderingApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace Buildering.Data.DAO
{
    public class CommentDAO : ICommentDAO
    {

        public void AddCommentToRoute(Comment comment, int id, RouteDBContext context)
        {
            //Adding id from route to the comment
            comment.RouteId = id;
  
            context.Comments.Add(comment);
        }

        public Comment GetComment(int id, RouteDBContext context)
        {
            return context.Comments.Find(id);
        }

        public IList<Comment> GetComments(Route route, RouteDBContext context)
        {
            return route.Comments.ToList();
        }

        public void EditComment(Comment comment, RouteDBContext context)
        {
            //Attaching the modified entry to context
            context.Entry(comment).State = EntityState.Modified;
        }

        public void DeleteComment(Comment comment, RouteDBContext context)
        {
            context.Comments.Remove(comment);
        }
    }
}

