using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoursesSystem.Data.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public List<StudentsCourses> Students { get; set; } = new List<StudentsCourses>();

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Credits { get; set; }

        public ApplicationUser Trainer { get; set; }

        public string TrainerId { get; set; }

        public List<Feedback> Reviews { get; set; }

    }
}
