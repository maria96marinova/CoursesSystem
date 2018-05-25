using AutoMapper.QueryableExtensions;
using CoursesSystem.Services;
using CoursesSystem.Web.Infrastructure;
using CoursesSystem.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesSystem.Web.Controllers
{
    [Authorize(Roles =RolesConstants.Administratior)]
    public class UserController:Controller
    {
        private readonly IUserService userService;
        

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult All()
        {
         
              return View(new UserViewModel
            {
                Users= this.userService.All(),
                Roles=this.userService.Roles().Select(c=>new SelectListItem
                {
                    Value=c.Name,
                    Text=c.Name
                })

        });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(string userId,string role)
        {
           var result= await this.userService.AssignToRole(userId, role);

            if (result)
            {
                TempData["Success"] = $"User successfully added to role {role}";
                return RedirectToAction(nameof(All));
            }

            return NotFound();

        }

        public async Task<IActionResult> Roles(string userId)
        => View(await this.userService.GetRoles(userId));

    }
}
