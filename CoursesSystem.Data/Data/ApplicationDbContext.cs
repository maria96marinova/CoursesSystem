using CoursesSystem.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CoursesSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentsCourses> StudentsCourses { get; set; }
        public DbSet<Feedback> Reviews { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentsCourses>()
                .HasKey(pk => new { pk.StudentId, pk.CourseId });

            builder.Entity<StudentsCourses>()
                .HasOne(c => c.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.StudentId);

            builder.Entity<StudentsCourses>()
                .HasOne(c => c.Course)
                .WithMany(p => p.Students)
                .HasForeignKey(a => a.CourseId);

            builder.Entity<Course>()
                .HasOne(c => c.Trainer)
                .WithMany(p => p.Trainings)
                .HasForeignKey(c => c.TrainerId);

            builder.Entity<Feedback>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Reviews)
                .HasForeignKey(a => a.CourseId);


                
                



           
        }
    }
}
