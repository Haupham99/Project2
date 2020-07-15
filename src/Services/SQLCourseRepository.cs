using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    public class SQLCourseRepository : ICourseRepository
    {
        private AppDbContext _context;

        public SQLCourseRepository(AppDbContext context)
        {
            _context = context;
        }
        public Course AddCourse(Course course)
        {
            _context.Course.Add(course);
            _context.SaveChanges();
            return course;
        }

        public Course DeleteCourse(int courseId)
        {
            Course course = _context.Course.Find(courseId);
            if(course != null)
            {
                _context.Course.Remove(course);
                _context.SaveChanges();
            }
            return course;
        }

        public Course EditCourse(Course course)
        {
            var cours = _context.Course.Attach(course);
            cours.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return course;
        }

        public List<Course> GetAllCourse()
        {
            return _context.Course.ToList();
        }

        public Course GetCourse(int courseId)
        {
            return _context.Course.Find(courseId);
        }

        public List<Category> GetAllCategory()
        {
            return _context.Category.ToList();
        }
        public List<Course> GetCourseByCategory(int id)
        {
            return _context.Course.Where(a => (a.category_id == id)).ToList();
        }

        public List<Course> GetMyCourse(int id)
        {
            List<AccountCourse> accountCourses =  _context.AccountCourse.Where(a => (a.accountId == id)).ToList();
            List<Course> courses = new List<Course>();
            foreach(var accountCourse in accountCourses)
            {
                courses.Add(_context.Course.Where(a => a.id == accountCourse.courseId).FirstOrDefault());
            }
            return courses;
        }

        public AccountCourse AddCourse(int courseId, int accountId)
        {
            AccountCourse accountCourse = new AccountCourse
            {
                courseId = courseId,
                accountId = accountId
            };
            _context.AccountCourse.Add(accountCourse);
            _context.SaveChanges();
            return accountCourse;
        }
    }
}
