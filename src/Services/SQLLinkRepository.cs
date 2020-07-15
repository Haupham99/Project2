using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    public class SQLLinkRepository : ILinkRepository
    {
        private AppDbContext _context;

        public SQLLinkRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Link> getLinkByCourseId(int id)
        {
            return _context.Link.Where(a => (a.courseId == id)).ToList();
        }
    }
}
