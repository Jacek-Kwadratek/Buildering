using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Services.IService
{
    public interface IUserService
    {
        void AddUser(User user);
        IList<User> GetUsers();
        User GetUserByEmail(string email);
        void DeleteUser(string email);
    }
}
