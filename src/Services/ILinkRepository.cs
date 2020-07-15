using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    public interface ILinkRepository
    {
        List<Link> getLinkByCourseId(int id);
    }
}
