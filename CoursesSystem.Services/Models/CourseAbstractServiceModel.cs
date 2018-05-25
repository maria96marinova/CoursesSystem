using System;

namespace CoursesSystem.Services.Models
{
    public class CourseAbstractServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Trainer { get; set; }
    }
}
