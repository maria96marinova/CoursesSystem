using AutoMapper;
using CoursesSystem.Services.Models;
using CoursesSystem.Web.Models.User;

namespace CoursesSystem.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<UsersListingModel, UserViewModel>();
        }
    }
}
