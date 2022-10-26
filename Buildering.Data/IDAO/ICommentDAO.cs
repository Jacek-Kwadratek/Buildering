using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Data.IDAO
{
    public interface ICommentDAO
    {
        void AddCommentToRoute(Comment comment, int id, RouteDBContext context);
        Comment GetComment(int id, RouteDBContext context);
        IList<Comment> GetComments(Route route, RouteDBContext context);
        void EditComment(Comment comment, RouteDBContext context);
        void DeleteComment(Comment comment, RouteDBContext context);
    }
}
