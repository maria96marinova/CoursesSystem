namespace CoursesSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure;
    using CoursesSystem.Services;
    using Microsoft.AspNetCore.Identity;
    using CoursesSystem.Data.Models;
    using System.Linq;
    using CoursesSystem.Web.Models.User;
    using CoursesSystem.Web.Models.Trainer;

    [Authorize(Roles =RolesConstants.Trainer)]
    public class TrainerController:Controller
    {
        private readonly ITrainerService trainerService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICourseService courseService;

        public TrainerController(ITrainerService trainerService,UserManager<ApplicationUser> userManager,
            ICourseService courseService)
        {
            this.trainerService = trainerService;
            this.userManager = userManager;
            this.courseService = courseService;
        }


        public IActionResult AllCourses()
        {
            var user = this.HttpContext.User;
            string trainerId =  this.userManager.GetUserId(user);
            return View("All",this.trainerService.All(trainerId));

        }

        [Route("/trainer/students/{courseId}")]
        public IActionResult Students(int courseId)
        {
            var user = this.HttpContext.User;
            string trainerId = this.userManager.GetUserId(user);

            this.trainerService.Students(courseId, trainerId);

            var result = new StudentWithGradeModel
            {
                Students = this.trainerService.Students(courseId, trainerId),
                CourseId=courseId
            };
              

            return View(result);

        }

        [Route("/trainer/course/reviews/{courseId}")]
        public IActionResult Reviews(int courseId)
        => View(new ReviewBasicViewModel
        {
            CourseTitle=this.courseService.CourseNameById(courseId),
            CourseId = courseId,
            Reviews = this.courseService.ReviewsByCourse(courseId)
        });

        [HttpPost]
        public IActionResult AssignGrade(int courseId, string studentId,Grade grade)
        {
           var result= this.trainerService.AssignGrade(courseId, studentId, grade);
            if (!result)
            {
                return NotFound();
            }



            TempData["Success"] = "Grade successfully assigned";


            return RedirectToAction(nameof(Students), new {courseId=courseId });
        }
    }
}
