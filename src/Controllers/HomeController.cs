using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.Build.WebApi;
using Project2.Models;
using Project2.Services;
using Project2.ViewModel;

namespace Project2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILinkRepository _linkRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment, ICourseRepository courseRepository, ILinkRepository linkRepository)
        {
            this.webHostEnvironment = webHostEnvironment;
            _courseRepository = courseRepository;
            _linkRepository = linkRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Home()
        {
            List<Course> courses = _courseRepository.GetAllCourse();
            List<Course> specialCourses = new List<Course>
            {
                _courseRepository.GetCourse(1),
                _courseRepository.GetCourse(2)
            };
            ViewBag.specialCourses = specialCourses;
            return View(courses);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Course(int id)
        {
            
            List<Link> links1 = _linkRepository.getLinkByCourseId(id);
            Course course1 = _courseRepository.GetCourse(id);
            bool isBuyed = false;
            var accountId = Request.Cookies["id"];
            if(accountId != null)
            {
                var check = 0;
                List<Course> courses = _courseRepository.GetMyCourse(Int32.Parse(accountId));
                foreach(var course in courses)
                {
                    if (course == course1) 
                    {
                        check = 1;
                        break;
                    } 
                }
                if (check == 1) isBuyed = true;
            }
            CourseViewModel courseViewModel = new CourseViewModel
            {
                course = course1,
                links = links1,
                isBuyed = isBuyed
            };
            return View(courseViewModel);
        }
        [Route("Course")]
        [Route("Courses")]
        public IActionResult Courses()
        {
            List<Course> courses1 = _courseRepository.GetAllCourse();
            List<Category> categories1 = _courseRepository.GetAllCategory();
            CoursesViewModel coursesViewModel = new CoursesViewModel
            {
                courses = courses1,
                categories = categories1
            };
            return View(coursesViewModel);
        }
        public IActionResult Single_post()
        {
            return View();
        }
        [Route("course/category/{id}")]
        public List<Course> GetCourseByCategory(int id)
        {
            List<Course> courses = _courseRepository.GetCourseByCategory(id);
            return courses;
        }
        [Route("mycourse/{id}")]
        public IActionResult MyCourse(int id)
        {
            List<Course> courses = _courseRepository.GetMyCourse(id);
            MyCourseViewModel myCourseViewModel = new MyCourseViewModel
            {
                courses = courses,
                success = null
            };
            return View(myCourseViewModel);
        }

        public IActionResult Study(int id)
        {
            var username = Request.Cookies["id"];
            if(username != null)
            {
                var courses = _courseRepository.GetMyCourse(Int32.Parse(username));
                List<Link> links1 = _linkRepository.getLinkByCourseId(id);
                Course course1 = _courseRepository.GetCourse(id);
                foreach (var course in courses)
                {
                    if(course == course1)
                    {
                        CourseViewModel courseViewModel = new CourseViewModel
                        {
                            course = course1,
                            links = links1
                        };
                        return View(courseViewModel);
                    }
                }
            }
            else
            {
                return Redirect("/login");
            }
            return Redirect("/");
        }

        public IActionResult AddCourse(int courseId)
        {
            var accountId = Request.Cookies["id"];
            _courseRepository.AddCourse(courseId, Int32.Parse(accountId));
            HttpContext.Session.SetString("success", "Đăng ký khóa học thành công");
            List<Course> courses = _courseRepository.GetMyCourse(Int32.Parse(accountId));
            MyCourseViewModel myCourseViewModel = new MyCourseViewModel
            {
                courses = courses,
                success = HttpContext.Session.GetString("success")
            };
            return View("MyCourse", myCourseViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
