using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Models
{
    public class Course
    {
        public int id { get; set; }
        public int category_id { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string teacher { get; set; }
        //public string created_by { get; set; }
        //public DateTime created_date { get; set; }
        //public string modified_by { get; set; }
        //public DateTime modified_date { get; set; }
    }
}
