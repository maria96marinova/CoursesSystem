namespace CoursesSystem.Services.Implementations
{
    using Models;
    using Data;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data.Models;

    public class TrainerService : ITrainerService
    {
        private readonly ApplicationDbContext db;

        public TrainerService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool AddAssignment(int courseId, string trainerId, string content)
        {
            var isTrainerForCourse = this.db.Courses.Any(c => c.Id == courseId && c.TrainerId == trainerId);
            if (!isTrainerForCourse)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<CourseTrainerModel> All(string trainerId)
        => this.db.Courses.Where(c => c.TrainerId == trainerId).ProjectTo<CourseTrainerModel>()
            .ToList();

        public bool AssignGrade(int courseId, string studentId,Grade grade)
        {
            var result=this.db.StudentsCourses.Where(c => c.CourseId == courseId && c.StudentId == studentId)
                .FirstOrDefault();
            if (result==null)
            {
                return false;
            }

            result.Grade = grade;
            this.db.SaveChanges();

            return true;

           
        }

        

        public IEnumerable<StudentTrainerModel> Students(int courseId, string trainerId)
        {
            var result= this.db.StudentsCourses.Where(c => c.CourseId == courseId && c.Course.TrainerId == trainerId)
                .Select(c => new StudentTrainerModel
                {
                    Id = c.StudentId,
                    Name = c.Student.UserName,
                    LetterGrade = (Grade)c.Grade


                }).ToList();

            return result;
        }
    }



}
