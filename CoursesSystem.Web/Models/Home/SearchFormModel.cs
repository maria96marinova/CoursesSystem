using System.ComponentModel.DataAnnotations;

namespace CoursesSystem.Web.Models.Home
{
    public class SearchFormModel
    {
        public string SearchText { get; set; }

        [Display(Name = "Courses")]
        public bool SearchInCourses { get; set; } = true;



        [Display(Name = "Users")]
        public bool SearchInUsers { get; set; } = true;
    }
}
