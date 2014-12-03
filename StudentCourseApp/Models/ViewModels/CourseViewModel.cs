using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCourseApp.Models.ViewModels
{
    public class CourseViewModel
    {
        public bool IsSelected { get; set; }

        public int CourseId { get; set; }

        public string Name { get; set; }

        public string TeacherName { get; set; }

        public string MeetingDates { get; set; }

        public DateTime MeetingTime { get; set; }

        public string Location { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int CreatedUserId { get; set; }

        public int ModifiedUserId { get; set; }

        public string Occupancy { get; set; }
    }
}