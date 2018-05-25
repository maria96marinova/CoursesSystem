namespace CoursesSystem.Services.Models
{
    using CoursesSystem.Data.Models;
    using System;

    public class CourseTrainerModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Grade Grade { get; set; }
    }
}
