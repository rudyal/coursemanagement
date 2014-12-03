using StudentCourseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentCourseApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            // validate user
            if (user == null)
            {
                ViewBag.ErrorMessage = "Provide Login info";
                return View();
            }

            // authenticate user
            bool IsAuthenticated = false;
            using (StudentCourseContext context = new StudentCourseContext())
            {
                IsAuthenticated = context.Users
                    .Where(x => x.Email == user.Email && x.Password == user.Password)
                    .Any();
            }

            // store creditials in cookie
            if (IsAuthenticated)
            {
                // Create Cookie
                HttpCookie authCook = System.Web.Security.FormsAuthentication.GetAuthCookie(user.Email, false);
                Response.Cookies.Add(authCook);
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Login";
                return View();
            }

            //redirect
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
