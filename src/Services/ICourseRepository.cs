using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    public interface ICourseRepository
    {
        Course GetCourse(int courseId);
        List<Course> GetAllCourse();
        Course AddCourse(Course course);
        Course EditCourse(Course course);
        Course DeleteCourse(int courseId);
        List<Category> GetAllCategory();
        List<Course> GetCourseByCategory(int id);
        List<Course> GetMyCourse(int id);
        AccountCourse AddCourse(int courseId, int accountId);
    }
}
