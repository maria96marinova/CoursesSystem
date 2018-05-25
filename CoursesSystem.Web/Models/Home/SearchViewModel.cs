using CoursesSystem.Data.Models;
using System;
using System.Collections.Generic;

namespace CoursesSystem.Web.Models.Home
{
    public class SearchViewModel:HomeIndexViewModel
    {
        public IEnumerable<UserBasicModel> Users { get; set; } = new List<UserBasicModel>();

        

    }
}
