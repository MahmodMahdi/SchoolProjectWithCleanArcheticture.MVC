using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
using System.ComponentModel.DataAnnotations;
namespace BusinessLogicLayer.Validations
{
    public class UniqueDepartmentAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DepartmentViewModel courseVM = (DepartmentViewModel)validationContext.ObjectInstance;
            ApplicationDbContext db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext))!;
            string? name = value?.ToString();
            Department? department = db?.Departments.FirstOrDefault(c => c.Name == name && c.Id != courseVM.Id);
            if (department == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Name already exist");
        }
    }
}
