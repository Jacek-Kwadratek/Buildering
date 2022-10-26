using Buildering.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuilderingApp.Models
{
    public class Video
    {
        public int VideoId { get; set; }
        public string URL { get; set; }
        public int RouteId { get; set; }
        public string User { get; set; }
    }
}
