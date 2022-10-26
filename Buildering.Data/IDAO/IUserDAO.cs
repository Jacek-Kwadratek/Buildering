using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Data.IDAO
{
    public interface IUserDAO
    {
        void AddUser(User user, RouteDBContext context);
        User GetUserByEmail(string email, RouteDBContext context);
        void DeleteUser(User user, RouteDBContext context);
        IList<User> GetUsers(RouteDBContext context);
        bool CheckIfGraded(Route route, User user);
        void AddGradedRoute(Route route, string userId, RouteDBContext context);
    }
}
