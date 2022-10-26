using Buildering.Data.DAO;
using Buildering.Data.IDAO;
using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using Buildering.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Services.Service
{
    public class UserService : IUserService
    {
        private IUserDAO userDAO;

        public UserService()
        {
            userDAO = new UserDAO();
        }
        public void AddUser(User user)
        {
                using (RouteDBContext context = new RouteDBContext())
                {
                    userDAO.AddUser(user, context);
                    context.SaveChanges();
                }
        }
        public void DeleteUser(string email)
        {
            using (RouteDBContext context = new RouteDBContext())
            {
                User user = userDAO.GetUserByEmail(email, context);
                userDAO.DeleteUser(user, context);
                context.SaveChanges();
            }
        }

        public User GetUserByEmail(string email)
        {
            using (RouteDBContext context = new RouteDBContext())
            {
                return userDAO.GetUserByEmail(email, context);
            }
        }

        public IList<User> GetUsers()
        {
            using(RouteDBContext context = new RouteDBContext())
            {
                return userDAO.GetUsers(context);
            }
        }

    }
}
