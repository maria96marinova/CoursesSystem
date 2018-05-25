namespace CoursesSystem.Services
{
    using System.Collections.Generic;
    using Models;
    using Data.Models;

    public interface ITrainerService
    {
        IEnumerable<CourseTrainerModel> All(string trainerId);
        IEnumerable<StudentTrainerModel> Students(int courseId,string trainerId);
        bool AssignGrade(int courseId, string studentId,Grade grade);
        bool AddAssignment(int courseId, string trainerId, string content);
        
    }
}
