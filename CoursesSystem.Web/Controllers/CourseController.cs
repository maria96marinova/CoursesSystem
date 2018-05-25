using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoursesSystem.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesSystem.Services;
using CoursesSystem.Services.Models;
using CoursesSystem.Web.Models.Course;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using CoursesSystem.Data.Models;

namespace CoursesSystem.Web.Controllers
{

    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private UserManager<ApplicationUser> userManager;

        public CourseController(ICourseService courseService, IUserService userService, UserManager<ApplicationUser> userManager)
        {
            this.courseService = courseService;
            this.userService = userService;
            this.userManager = userManager;
        }



        [Authorize(Roles = RolesConstants.Administratior)]
        public async Task<IActionResult> Create()
        {
            var trainers = await this.userService.Trainers();

            return View(new CourseFormModel
            {
                Trainers = trainers.Select(t => new SelectListItem
                {
                    Value = t.TrainerId,
                    Text = t.Name
                }),

                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30)
            });
        }

        [Authorize(Roles = RolesConstants.Administratior)]
        [HttpPost]
        public async Task<IActionResult> Create(CourseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                var trainers = await this.userService.Trainers();


                model.Trainers = trainers
                                .Select(t => new SelectListItem
                                {
                                    Value = t.TrainerId,
                                    Text = t.Name
                                });

                return View(model);
            }

            await this.courseService.CreateAsync(model.Tilte, model.Description,
                model.StartDate, model.EndDate,model.Credits, model.TrainerId);

            return RedirectToAction("Index", "Home");

        }


        public IActionResult Details(int courseId)
        {
            var model = this.courseService.Details(courseId);
            model.isStudentRegistered = this.courseService.isRegisteredInCourse(model.Id,
                this.userManager.GetUserId(this.User));

            return View(model);

        }

        [HttpPost]
        [Authorize]
        public IActionResult Register([FromForm]int courseId)
        {
            var result = this.courseService.Register(courseId, this.userManager.GetUserId(this.User));
            if (!result)
            {
                return NotFound();
            }


            TempData["Success"] = "Thank you for taking this course";
            return RedirectToAction("Index", "Home");


        }

        [HttpPost]
        [Authorize]
        public IActionResult SignOut(int courseId, string userId)
        {
            var result = this.courseService.SignOut(courseId, this.userManager.GetUserId(this.User));
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Courses()
        => View(this.courseService.CoursesByStudent(this.userManager.GetUserId(this.User)));

        [Authorize]
        public IActionResult Review()
        {
            FeedbackFormModel model = new FeedbackFormModel
            {
                Courses = CoursesForReviewing()
            };


            return View(model);
        }

        
        [HttpPost]
        [Authorize]
        public IActionResult Review(FeedbackFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var studentId=this.userManager.GetUserId(this.User);

            var result = this.courseService.ReviewCourse(model.CourseId,studentId, model.Content);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Courses));


        }

        private IEnumerable<SelectListItem> CoursesForReviewing()
            => this.courseService.CoursesForReview(this.userManager.GetUserId(this.User))
                            .Select(c => new SelectListItem
                            {
                                Value = c.Id.ToString(),
                                Text = c.Title
                            });


            

        }

    }

         

      




