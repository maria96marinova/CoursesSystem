using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using CoursesSystem.Data;
using CoursesSystem.Data.Models;
using CoursesSystem.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoursesSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
  

        public UserService(ApplicationDbContext db,UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {

            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        
        public IEnumerable<UsersListingModel> All()
        
        =>this.db.Users.ProjectTo<UsersListingModel>().ToList();


        public async Task<bool> AssignToRole(string userId, string role)
        {
            var user =await this.userManager.FindByIdAsync(userId);
            var roleName = await this.roleManager.FindByNameAsync(role);

            if (user==null || roleName==null)
            {
                return false;
            }

            await this.userManager.AddToRoleAsync(user, role);

            return true;
        }

        public IEnumerable<UserBasicModel> ByName(string name)
        {
            name = name ?? string.Empty;

            return this.db.Users.Where(u => u.UserName.ToLower().Contains(name))
            .OrderBy(u => u.UserName)
            .ProjectTo<UserBasicModel>()
            .ToList();
        }

        public async Task<IEnumerable<string>> GetRoles(string userId)
        {
            var user=await this.userManager.FindByIdAsync(userId);
            var result= await this.userManager.GetRolesAsync(user);
            return result;
            
        }

        public IEnumerable<RoleSimpleModel> Roles()
        => this.roleManager.Roles.ProjectTo<RoleSimpleModel>().ToList();

        public async Task<IEnumerable<TrainerViewModel>> Trainers()
        {
            var result=await this.userManager.GetUsersInRoleAsync("Trainer");
            return result.Select(t => new TrainerViewModel
            {
                TrainerId = t.Id,
                Name = t.UserName
            });
        }







    }
}
