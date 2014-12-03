using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentCourseApp.Models
{
    [Table("Registration")]
    public class RegistrationItem
    {
        [Key]
        public int RegistrationID { get; set; }
        public User user { get; set; }
        [ForeignKey("user")]
        public int UserId { get; set; }
        public Course course { get; set; }
        [ForeignKey("course")]
        public int CourseId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedUserId { get; set; }
        public int ModifiedUserId { get; set; }
        
    }
}