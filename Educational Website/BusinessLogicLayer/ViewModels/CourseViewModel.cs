using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BusinessLogicLayer.Validations;
using Microsoft.AspNetCore.Mvc;
namespace Educational_Website.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(30, ErrorMessage = "Name must be less than 29 letters"), MinLength(2, ErrorMessage = "Name must be greater than 2 letters.")]
        [UniqueCourse] //by custom attribute and it work in server side only not client side
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "*")]
        [Range(50, 100, ErrorMessage = "{0} must be {1} to {2}")]
        public int Grade { get; set; }
        [Required(ErrorMessage = "*")]
        [Range(0, 100, ErrorMessage = "{0} must be {1} to {2}")]
        [Remote("CheckGrade", "Course"
            , AdditionalFields = "Grade"
            , ErrorMessage = "MinDegree must be less than Degree")]
        public int MinDegree { get; set; }
        [DisplayName("Department")]
        [Required(ErrorMessage = "*")]
        public int? dept_id { get; set; }
    }
}
