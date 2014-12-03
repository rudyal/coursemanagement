using StudentCourseApp.Models;
using StudentCourseApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentCourseApp.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [HttpGet]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserManagement()
        {
            // GET all Courses
            List<User> Users;

            using (StudentCourseContext context = new StudentCourseContext())
            {
                Users = context.Users.ToList();
            }

            //List<UserViewModel> vmUsers = new List<UserViewModel>();
            int p = 0;
            return View(Users);

        }

        [HttpGet]
        public ActionResult CourseManagement()
        {
            // GET all Courses
            List<Course> Courses;

            using (StudentCourseContext context = new StudentCourseContext())
            {
                Courses = context.Course.ToList();
            }

            //List<UserViewModel> vmUsers = new List<UserViewModel>();
            int p = 0;
            return View(Courses);

        }

        [HttpGet]
        public ActionResult CourseCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CourseCreate(Course course)
        {
            StudentCourseContext context = new StudentCourseContext();
            try
            {
                context.Course.Add(course);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return Content("Fail" + e);
            }
        }

        [HttpGet]
        public ActionResult UserCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserCreate(User user)
        {
            StudentCourseContext context = new StudentCourseContext();
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return Content("Fail" + e);
            }
        }
	}
}