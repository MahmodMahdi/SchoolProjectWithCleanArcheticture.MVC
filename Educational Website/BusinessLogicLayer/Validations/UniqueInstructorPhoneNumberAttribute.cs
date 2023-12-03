using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
using System.ComponentModel.DataAnnotations;
namespace BusinessLogicLayer.Validations
{
    public class UniqueInstructorPhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            InstructorViewModel instructorVM = (InstructorViewModel)validationContext.ObjectInstance;
            ApplicationDbContext db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext))!;
            string? phone = value?.ToString();

            Instructor? instructor = db?.Instructors.FirstOrDefault(c => c.PhoneNumber == phone && c.Id != instructorVM.Id);
            if (instructor == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Phone Number already exist");
        }
    }
}
