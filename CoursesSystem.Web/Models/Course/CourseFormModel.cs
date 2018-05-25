using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesSystem.Web.Models.Course
{
    public class CourseFormModel:IValidatableObject
    {
        [Required]
        public string Tilte { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        public int Credits { get; set; }
            

        public string TrainerId { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate >= this.EndDate || this.StartDate < DateTime.UtcNow)
            {
                yield return new ValidationResult("The dates are invalid");
            }
        }
    }
}
