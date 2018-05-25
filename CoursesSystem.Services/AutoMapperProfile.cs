using AutoMapper;
using CoursesSystem.Data.Models;
using CoursesSystem.Services.Models;
using Microsoft.AspNetCore.Identity;

namespace CoursesSystem.Services
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<ApplicationUser, UsersListingModel>();
            this.CreateMap<IdentityRole, RoleSimpleModel>();
            this.CreateMap<Course, CourseServiceModel>()
                .ForMember(c => c.Trainer, map => map.MapFrom(p => p.Trainer.UserName));
            this.CreateMap<Course, CourseTrainerModel>();
            this.CreateMap<Course,CourseBasicModel>()
                .ForMember(c => c.Trainer, map => map.MapFrom(p => p.Trainer.UserName));
       
            this.CreateMap<ApplicationUser, StudentTrainerModel>();

            this.CreateMap<ApplicationUser, UserBasicModel>()
                .ForMember(u => u.Courses, map => map.MapFrom(c => c.Courses.Count));


            
                
                
        }
    }
}

