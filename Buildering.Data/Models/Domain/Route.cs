using BuilderingApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Buildering.Data.Models.Domain
{
    public class Route
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Coordinate { get; set; }
        public string Description { get; set; }
        //Storing Topo Path in DB
        public string Topo { get; set; }
        //Storing Topo Name in DB
        public string TopoName { get; set; }
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Rating { get; set; }
        public Grades Grade { get; set; }
        public Grades VotedGrade { get; set; }
        public double VotedGradeNum { get; set; }
        public int VotedGradeCount { get; set; }
        public string User { get; set; }
        
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Video> Videos { get; set; }

        public enum Grades { V0 = 1, V1 = 2, V2 = 3, V3 = 4, V4 = 5, V5 = 6,
            V6 = 7, V7 = 8, V8 = 9, V9 = 10, V10 = 11, V11 = 12, V12 = 13,
            V13 = 14, V14 = 15, V15 = 16 };
    }
    



}
