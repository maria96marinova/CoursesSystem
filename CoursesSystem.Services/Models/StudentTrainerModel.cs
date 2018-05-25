using CoursesSystem.Data.Models;

namespace CoursesSystem.Services.Models
{
    public class StudentTrainerModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Grade? LetterGrade { get; set; }
    }
}
