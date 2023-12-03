using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
using System.ComponentModel.DataAnnotations;
namespace BusinessLogicLayer.Validations
{
    public class UniqueTraineeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            TraineeViewModel TraineeVM = (TraineeViewModel)validationContext.ObjectInstance;
            ApplicationDbContext db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext))!;
            string? Name = value?.ToString();
            Trainee? Trainee = db?.Trainees.FirstOrDefault(T => T.Name == Name && T.Id != TraineeVM.Id && T.PhoneNumber == TraineeVM.PhoneNumber);
            if (Trainee == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Item already exist");
        }
    }
}
