using Buildering.Data.Models.Domain;
using Buildering.Data.Models.Repository;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Buildering.Services.IService
{
    public interface IRouteService
    {
        void AddRoute(HttpPostedFileBase topo, Route route);
        Route GetRoute(int id);
        IList<Route> GetRoutes();
        IEnumerable<Route> SearchRoutes(Nullable<Buildering.Data.Models.Domain.Route.Grades> grading, string searchstring);
        void EditRoute(HttpPostedFileBase topo, Route route);
        void DeleteRoute(int id);
        void ContactAdmin(string name, string subject, string message, string returnUrl);
        bool VoteOnGrade(int routeId, int grade, string userEmail);
        
    }
}
