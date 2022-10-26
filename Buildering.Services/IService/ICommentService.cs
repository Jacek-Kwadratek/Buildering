using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Services.IService
{
    public interface ICommentService
    {
        void AddCommentToRoute(Comment comment, int id);
        Comment GetComment(int id);
        IList<Comment> GetComments(int id);
        void EditComment(Comment comment);
        void DeleteComment(int id);
    }
}
