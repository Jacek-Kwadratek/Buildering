using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Buildering.Data.IDAO
{
    public interface IRouteDAO
    {
        void AddRoute(HttpPostedFileBase topo, Route route, RouteDBContext context);
        Route GetRoute(int id, RouteDBContext context);
        IList<Route> GetRoutes(RouteDBContext context);
        void EditRoute(HttpPostedFileBase topo, Route route, RouteDBContext context);
        void DeleteRoute(Route route, RouteDBContext context);
        void VoteOnGrade(RouteDBContext context, Route route, int grade);
    }
}
