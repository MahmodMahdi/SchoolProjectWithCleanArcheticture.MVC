using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
using System.ComponentModel.DataAnnotations;
namespace BusinessLogicLayer.Validations
{
    public class UniqueResultAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            CourseResultViewModel CourseResultVM = (CourseResultViewModel)validationContext.ObjectInstance;
            ApplicationDbContext db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext))!;
            int? name = (int)value!;
            CourseResult? CourseResult = db?.CourseResult.FirstOrDefault(c => c.trainee_id == name && c.Id != CourseResultVM.Id && c.crs_id != CourseResultVM.crs_id);
            if (CourseResult == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Result already exist");
        }
    }
}
