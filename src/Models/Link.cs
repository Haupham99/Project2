using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Models
{
    public class Link
    {
        public int id { get; set; }
        public int courseId { get; set; }
        public string longTime { get; set; }
        
        public string src { get; set; }
        public string name { get; set; }
    }
}
