namespace CoursesSystem.Web.Models.User
{
    using CoursesSystem.Data.Models;
    using CoursesSystem.Services.Models;
    using System.Collections.Generic;
  
    public class StudentWithGradeModel
    {
       public IEnumerable<StudentTrainerModel> Students { get; set; }
       public int CourseId { get; set; }

    }
}
