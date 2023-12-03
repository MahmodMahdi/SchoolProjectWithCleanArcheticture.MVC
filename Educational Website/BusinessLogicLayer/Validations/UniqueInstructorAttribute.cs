using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
using System.ComponentModel.DataAnnotations;
namespace BusinessLogicLayer.Validations
{
    public class UniqueInstructorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            InstructorViewModel instructorVM = (InstructorViewModel)validationContext.ObjectInstance;
            ApplicationDbContext db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext))!;
            string? Instructor = value?.ToString();

            Instructor? instructor = db?.Instructors.FirstOrDefault(c => c.Name == Instructor && c.Id != instructorVM.Id && c.crs_id == instructorVM.crs_id);
            if (instructor == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Item already exist");
        }
    }
}
