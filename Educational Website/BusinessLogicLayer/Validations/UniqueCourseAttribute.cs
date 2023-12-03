using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
using System.ComponentModel.DataAnnotations;
namespace BusinessLogicLayer.Validations
{
    public class UniqueCourseAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            CourseViewModel courseVM = (CourseViewModel)validationContext.ObjectInstance;
            ApplicationDbContext db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext))!;

            string? name = value?.ToString();

            Course? course = db?.Courses.FirstOrDefault(c => c.Name == name && c.Id != courseVM.Id && c.dept_id == courseVM.dept_id);
            if (course == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Item already exist");
        }
    }
}
