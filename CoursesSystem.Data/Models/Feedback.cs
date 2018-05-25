
namespace CoursesSystem.Data.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public ApplicationUser Student { get; set; }
        public string StudentId { get; set; }

    }
}
