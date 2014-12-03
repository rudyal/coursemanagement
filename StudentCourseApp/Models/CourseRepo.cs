using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCourseApp.Models
{
    public class CourseRepo
    {
        StudentCourseContext context = new StudentCourseContext();
        //public List<Models.Course> courseList { get; set; }

        public IEnumerable<Course> GetAllCourses()
        {
            return context.Course;
        }
    }
}