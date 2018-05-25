namespace CoursesSystem.Services.Models
{
    using Data.Models;
    public class CourseBasicModel:CourseAbstractServiceModel
    {
        public Grade? Grade { get; set; }
    }
}
