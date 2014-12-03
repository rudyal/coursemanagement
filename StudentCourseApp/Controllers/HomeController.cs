using StudentCourseApp.Models;
using StudentCourseApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity.EntityFramework;
using WebMatrix.WebData;

namespace StudentCourseApp.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Course()
        {

            // GET all Courses
            List<Course> Courses;

            using (StudentCourseContext context = new StudentCourseContext())
            {
                Courses = context.Course.ToList();
            }

            List<CourseViewModel> vmCourses = new List<CourseViewModel>();

            foreach (var dto in Courses)
            {
                vmCourses.Add(new CourseViewModel
                {
                    Name = dto.Name,
                    CourseId = dto.CourseId,
                    TeacherName = dto.TeacherName,
                    MeetingDates = dto.MeetingDates,
                    MeetingTime = dto.MeetingTime,
                    Occupancy = dto.Occupancy,
                    Location = dto.Location,
                    IsActive = dto.IsActive,
                    CreatedDate = dto.CreatedDate,
                    ModifiedDate = dto.ModifiedDate,
                    CreatedUserId = dto.CreatedUserId,
                    ModifiedUserId = dto.ModifiedUserId

                });
            }
            // GET current user ID
            int Currentid;
            string userName = User.Identity.GetUserName();
            if (userName == "")
            {
                return RedirectToAction("Login", "Account");
            }
            using (StudentCourseContext context = new StudentCourseContext())
            {
                Currentid = context.Users
                .Where(c => c.Email == userName)
                .Select(c => c.UserId).SingleOrDefault();
            }

            // Foreach course, IF user is registered for course, return filled Course
            foreach(var item in vmCourses){
                
                bool Cur;

                using (StudentCourseContext context = new StudentCourseContext())
                {
                    Cur = context.RegistrationItem
                    .Where(c => c.UserId == Currentid && c.CourseId == item.CourseId && c.IsActive == true )
                    .Any();
                }

                if(Cur==true){
                    item.IsSelected = true;
                }
                

            }
            if (TempData["shortMessage"] != null)
            {
                @ViewBag.Message = TempData["shortMessage"].ToString();
            }
            
            return View(vmCourses);
        }

        [HttpPost]
        public ActionResult Course(List<CourseViewModel> selectedCoures)
        {
            // For IsSelected make registration with courseID and UserID
            // Else make sure to unregister

            string selected= "f";
            string userName = User.Identity.GetUserName();
            
            int Currentid;
            using (StudentCourseContext context = new StudentCourseContext())
            {
                Currentid = context.Users
                .Where(c => c.Email == userName)
                .Select(c => c.UserId).SingleOrDefault();
            }
            

            foreach (var item in selectedCoures)
            {
                if (item.IsSelected==true)
                {
                    
                    using (StudentCourseContext context = new StudentCourseContext())
                    {
                        bool updatevar = false;
                        // Query the database for the row to be updated. 
                        var query =
                            from reg in context.RegistrationItem
                            where reg.CourseId == item.CourseId && reg.UserId == Currentid
                            select reg;

                        // Execute the query, and change the column values 
                        // you want to change. 
                        foreach (RegistrationItem reg in query)
                        {
                            updatevar = true;
                            reg.IsActive = true;
                            // Insert any additional changes to column values.
                        }

                        // Submit the changes to the database. 
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            // Provide for exceptions.
                        }

                        if (updatevar == false)
                        {
                            //Create a new instance of the reg object
                            RegistrationItem reg = new RegistrationItem();

                            //Add new values to each fields
                            reg.CourseId = item.CourseId;
                            reg.UserId = Currentid;
                            reg.IsActive = true;
                            reg.ModifiedUserId = Currentid;
                            reg.CreatedUserId = Currentid;
                            reg.CreatedDate = DateTime.Today;
                            reg.ModifiedDate = DateTime.Today;

                            //Insert the new Registration object
                            context.RegistrationItem.Add(reg);
                            context.SaveChanges();
                        }

                        
                    }

                }
                else
                {
                    // if there is registration item WITH courseid & userid, IsActive == false
                    using (StudentCourseContext context = new StudentCourseContext())
                    {
                        // Query the database for the row to be updated. 
                        var query =
                            from reg in context.RegistrationItem
                            where reg.CourseId == item.CourseId && reg.UserId == Currentid
                            select reg;

                        // Execute the query, and change the column values 
                        // you want to change. 
                        foreach (RegistrationItem reg in query)
                        {
                            reg.IsActive = false;
                            // Insert any additional changes to column values.
                        }

                        // Submit the changes to the database. 
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            // Provide for exceptions.
                        }
                    }
                }
                selected += item.Name + "<br />";
            }
            TempData["shortMessage"] = "Registration Saved.";
            return RedirectToAction("Course");
        }
    }
}
