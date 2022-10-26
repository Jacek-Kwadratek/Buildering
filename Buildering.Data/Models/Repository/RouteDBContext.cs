using Buildering.Data.Models.Domain;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Data.Models.Repository
{
    public class RouteDBContext : DbContext
    {
        public RouteDBContext() : base("RouteDBContext")
        {
            Database.SetInitializer(new BuilderingInitializer());
        }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
