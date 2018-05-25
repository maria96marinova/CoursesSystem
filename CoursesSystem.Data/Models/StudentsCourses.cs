using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesSystem.Data.Models
{
    public class StudentsCourses
    {
        public int CourseId { get; set; }

        public string StudentId { get; set; }

        public Course Course { get; set; }

        public ApplicationUser Student { get; set; }

        public Grade? Grade { get; set; }



    }
}
