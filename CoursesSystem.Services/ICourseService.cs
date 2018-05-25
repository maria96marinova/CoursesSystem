using CoursesSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoursesSystem.Services
{
    public interface ICourseService
    {
        Task CreateAsync(string title, string description,DateTime startDate,DateTime endDate,int credits, string trainerId);
        IEnumerable<CourseServiceModel> All();
        CourseServiceModel Details(int id);
        bool Register(int courseId, string userId);
        bool SignOut(int courseId, string userId);
        bool isRegisteredInCourse(int courseId, string userId);
        IEnumerable<CourseServiceModel> Courses(string name);
        IEnumerable<CourseBasicModel> CoursesByStudent(string studentId);
        IEnumerable<CourseServiceModel> ByName(string name);
        bool ReviewCourse(int courseId,string studentid,string content);
        bool ExistsCourse(int courseId);
        IEnumerable<CourseBasicModel> CoursesForReview(string studentId);
        IEnumerable<ReviewViewModel> ReviewsByCourse(int courseId);
        string CourseNameById(int courseId);


    }
}
