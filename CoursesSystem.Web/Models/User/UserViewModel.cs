using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using CoursesSystem.Services.Models;

namespace CoursesSystem.Web.Models.User
{
    public class UserViewModel
    {
        public IEnumerable<UsersListingModel> Users { get; set; }

        public int RoleId{ get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

      
    }
}
