
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoursesSystem.Web.Models.Course
{
    public class FeedbackFormModel
    {
        [Required]
        public string Content { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; } 


    }
}
