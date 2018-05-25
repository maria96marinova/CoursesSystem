using CoursesSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesSystem.Data;
using CoursesSystem.Services.Models;
using AutoMapper.QueryableExtensions;


namespace CoursesSystem.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext db;

        public CourseService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CourseServiceModel> All()
        {
           var courses= this.db.Courses.OrderByDescending(c => c.Id)
                .ProjectTo<CourseServiceModel>().ToList();

            foreach (var course in courses)
            {
                course.NumberStudents = this.db.StudentsCourses.Count(c => c.CourseId == course.Id);
            }


            return courses;
           
        }

        public IEnumerable<CourseServiceModel> ByName(string name)
        {
            name = name ?? string.Empty;

            return this.db.Courses.Where(c => c.Title.ToLower().Contains(name.ToLower()))
            .OrderByDescending(c => c.Title)
            .ProjectTo<CourseServiceModel>()
            .ToList();
        }

        public IEnumerable<CourseServiceModel> Courses(string name)
        => this.db.Courses.Where(c => c.Title.Contains(name))
            .ProjectTo<CourseServiceModel>().ToList();

        public IEnumerable<CourseBasicModel> CoursesByStudent(string studentId)
        =>this.db.StudentsCourses.Where(c => c.StudentId == studentId)
            .Select(c => new CourseBasicModel
            {
                Id = c.CourseId,
                Title = c.Course.Title,
                StartDate = c.Course.StartDate,
                EndDate = c.Course.EndDate,
                Description = c.Course.Description,
                Trainer = c.Course.Trainer.UserName,
                Grade=c.Grade
            }).ToList();

        public IEnumerable<CourseBasicModel> CoursesForReview(string studentId)
        {
            var courses = this.db.StudentsCourses.Where(c => c.StudentId == studentId)
                .Select(c => new CourseBasicModel
                {
                    Id = c.CourseId,
                    Title = c.Course.Title,
                    StartDate = c.Course.StartDate,
                    EndDate = c.Course.EndDate,
                    Description = c.Course.Description,
                    Trainer = c.Course.Trainer.UserName,
                    Grade = c.Grade

                }).OrderByDescending(c=>c.Title).ToList();

            var result = new List<CourseBasicModel>();

            foreach (var course in courses)
            {
                if(!ExistsReview(studentId,course.Id))
                {
                    result.Add(course);
                }
            }

            return result;



        }

        public async Task CreateAsync(string title, string description,
            DateTime startDate, DateTime endDate,int credits, string trainerId)
        {
            var course = new Course
            {
                Title = title,
                Description = description,
                StartDate=startDate,
                EndDate=endDate,
                Credits=credits,
                TrainerId = trainerId
            };

            this.db.Courses.Add(course);

            await this.db.SaveChangesAsync();
        }

        public CourseServiceModel Details(int id)
        {
            var course = this.db.Courses.Where(c => c.Id == id).ProjectTo<CourseServiceModel>().FirstOrDefault();
            course.NumberStudents = this.db.StudentsCourses.Count(c => c.CourseId == course.Id);
            return course;

        }

        public bool ExistsCourse(int courseId)
        => this.db.Courses.Any(c => c.Id == courseId);

        private bool ExistsReview(string studentId, int courseId)
        => this.db.Reviews.Any(r => r.CourseId == courseId && r.StudentId==studentId);

        public bool isRegisteredInCourse(int courseId, string userId)
        => this.db.StudentsCourses.Any(c => c.CourseId == courseId && c.StudentId == userId);

        public bool Register(int courseId, string userId)
        {
            var course = this.db.Courses.Find(courseId);

            if (course==null)
            {
                return false;
            }

            this.db.StudentsCourses.Add(new StudentsCourses
            {
                CourseId = courseId,
                StudentId = userId
            });

            this.db.SaveChanges();
            return true;
        }

        public bool ReviewCourse(int courseId,string studentId,string content)
        {
            if (!ExistsCourse(courseId))
            {
                return false;
            }



            this.db.Reviews.Add(new Feedback
            {
                CourseId = courseId,
                Content=content,
                StudentId=studentId
            });


            this.db.SaveChanges();
            return true;

          
        }

        public bool SignOut(int courseId, string userId)
        {
            var course = this.db.Courses.Any(c => c.Id == courseId);

            if (!course)
            {
                return false;
            }
            var result = this.db.StudentsCourses.Where(c => c.CourseId == courseId && c.StudentId == userId).FirstOrDefault();
            this.db.StudentsCourses.Remove(result);
            this.db.SaveChanges();
            return true;
        }


        public IEnumerable<ReviewViewModel> ReviewsByCourse(int courseId)
        => this.db.Reviews.Where(r => r.CourseId == courseId)
            .Select(r => new ReviewViewModel
            {
                
                Content = r.Content
            }).ToList();

        public string CourseNameById(int courseId)
        => this.db.Courses.Where(c => c.Id == courseId)
            .Select(c => c.Title).FirstOrDefault();
    }


}
