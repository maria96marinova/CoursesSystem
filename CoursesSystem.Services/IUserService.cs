using CoursesSystem.Data.Models;
using CoursesSystem.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoursesSystem.Services
{
    public interface IUserService
    {
        IEnumerable<UsersListingModel> All();
        Task<bool> AssignToRole(string userId, string role);
        IEnumerable<RoleSimpleModel> Roles();
        Task<IEnumerable<TrainerViewModel>> Trainers();
        Task<IEnumerable<string>> GetRoles(string userId);
        IEnumerable<UserBasicModel> ByName(string name);
     
    }
}
