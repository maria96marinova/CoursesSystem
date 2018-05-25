namespace CoursesSystem.Web.Models.Home
{
    using CoursesSystem.Services.Models;
    using System;
    using System.Collections.Generic;
  
    public class HomeIndexViewModel:SearchFormModel
    {
        public IEnumerable<CourseServiceModel> Courses { get; set; } = new List<CourseServiceModel>();


    }
}
