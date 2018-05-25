using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CoursesSystem.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<StudentsCourses> Courses { get; set; }=new List<StudentsCourses>();
        public List<Course> Trainings { get; set; } = new List<Course>();
        public List<Feedback> Reviews { get; set; } = new List<Feedback>();


        [Required]
        public DateTime BirthDate { get; set; }
        

    }
}
