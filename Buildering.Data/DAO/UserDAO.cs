using Buildering.Data.IDAO;
using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Buildering.Data.DAO
{
    public class UserDAO : IUserDAO
    {

        public void AddUser(User user, RouteDBContext context)
        {
            context.Users.Add(user);
        }

        public void AddGradedRoute(Route route, string user, RouteDBContext context)
        {
            context.Users.Find(user).GradedRoutes.Add(route);
        }

        public IList<User> GetUsers(RouteDBContext context)
        {
            return context.Users.ToList();
        }

        public User GetUserByEmail(string email, RouteDBContext context)
        {
            context.Users.Include(u => u.GradedRoutes).ToList();
            IQueryable<User> user = from u
                                    in context.Users
                                    where u.Email == email
                                    select u;
            return user.ToList().First();
        }

        public bool CheckIfGraded(Route route, User user)
        {
            for (int i = 0; i < user.GradedRoutes.Count(); i++)
            {
                if (user.GradedRoutes.ToList()[i].ID == route.ID)
                {
                    return true;
                }
            }
            return false;
        }

        public void DeleteUser(User user, RouteDBContext context)
        {
            context.Users.Remove(user);
        }

        public bool CheckIfUserNameInUse(string name, RouteDBContext context)
        {
            string[] userNames = context.Users.Select(User => User.Name).ToArray(); 
            for(int i = 0; i < userNames.Length; i++)
            {
                if (userNames[i] == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
