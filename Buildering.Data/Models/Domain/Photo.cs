using Buildering.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BuilderingApp.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Title { get; set; }
        [DisplayName("Upload File")]
        public string FilePath { get; set; }
        public string Name { get; set; }
        //Excluding PhotoFile attribute from database mapping 
        [NotMapped]
        //using HttpPostedFileBase to provide access
        //to individual uploaded files
        public HttpPostedFileBase PhotoFile { get; set; }
        
        public int RouteId { get; set; }
        public string User { get; set; }
    }
}
