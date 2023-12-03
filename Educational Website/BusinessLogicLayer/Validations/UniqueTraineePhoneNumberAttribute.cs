using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
using System.ComponentModel.DataAnnotations;
namespace BusinessLogicLayer.Validations
{
    public class UniqueTraineePhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            TraineeViewModel traineeVM = (TraineeViewModel)validationContext.ObjectInstance;
            ApplicationDbContext db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext))!;
            string? phone = value?.ToString();

            Trainee? trainee = db?.Trainees.FirstOrDefault(c => c.PhoneNumber == phone && c.Id != traineeVM.Id);
            if (trainee == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Phone Number already exist");
        }
    }
}
