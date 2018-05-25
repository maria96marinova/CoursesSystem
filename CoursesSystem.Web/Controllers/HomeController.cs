using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoursesSystem.Web.Models;
using CoursesSystem.Services;
using CoursesSystem.Web.Models.Home;

namespace CoursesSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public HomeController(ICourseService courseService,IUserService userService)
        {
            this.courseService = courseService;
            this.userService = userService;
        }

        public IActionResult Index()
        => View(new HomeIndexViewModel
        {
            Courses=this.courseService.All()
        });

        public IActionResult Search(SearchFormModel model)
        {
            var result = new SearchViewModel
            {
                SearchInCourses = model.SearchInCourses,
                SearchInUsers = model.SearchInUsers,
                SearchText = model.SearchText
            };

            if (model.SearchInCourses)
            {
                result.Courses = this.courseService.ByName(model.SearchText);
            }

            if (model.SearchInUsers)
            {
                result.Users = this.userService.ByName(model.SearchText);
            }


            return View(result);
        }
       

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
