using Buildering.Data.Models.Domain;
using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildering.Data.Models.Repository
{
    public class BuilderingInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RouteDBContext>
    {
        protected override void Seed(RouteDBContext context)
        {

            Route route1 = new Route()
            {
                ID = 1,
                Description = "climb up",
                User = "me",
                Grade = Route.Grades.V3,
                Coordinate = "53.37781949080278,-1.470623016357422",
                Name = "a climb",
                Rating = 4,
                //Adding a comment, video & photo through Route
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                         CommentId = 1,
                         Content = "A Comment",
                    },
                    new Comment(),
                    new Comment()
                    {
                        CommentId=2,
                        Content = "Comment 2",

                    }
                },
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                    PhotoId = 1,
                    Title = "A Photo"
                    }
                },
                Videos = new List<Video>()
                {
                    new Video()
                    {
                    VideoId = 1,
                    URL = "cxuS_EV4unE"
                    }
                }

            };
            Route route2 = new Route()
            {
                ID = 2,
                Description = "climb up2",
                User = "me2",
                Grade = Route.Grades.V2,
                Coordinate = "53.37781949080278,-1.570623016357422",
                Name = "a climb2",
                Rating = 2,
                Comments = new List<Comment>()
                {
                new Comment()
                    {
                         CommentId = 2,
                         Content = "Comment 3",
                    }
                },
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                    PhotoId = 2,
                    Title = "Photo 2"
                    }
                },
                Videos = new List<Video>()
                {
                    new Video()
                    {
                    VideoId = 2,
                    URL = "cxuS_EV4unE"
                    }
                }

            };
            context.Routes.Add(route1);
            context.Routes.Add(route2);
            context.SaveChanges();

        }


    }
    }
