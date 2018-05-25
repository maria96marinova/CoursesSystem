using CoursesSystem.Services.Models;
using System.Collections.Generic;

namespace CoursesSystem.Web.Models.Trainer
{
    public class ReviewBasicViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
    }
}
