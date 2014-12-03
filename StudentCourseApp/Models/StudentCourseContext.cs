using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentCourseApp.Models
{
    public class StudentCourseContext : DbContext
    {
        //public StudentCourseContext(){
        //    Database.SetInitializer<StudentCourseContext>(null);
        //}
        
        public DbSet<User> Users { get; set;}
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<RegistrationItem> RegistrationItem { get; set; }
    }
    
}