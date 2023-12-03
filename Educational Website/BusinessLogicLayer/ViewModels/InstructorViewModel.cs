using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BusinessLogicLayer.Validations;
namespace Educational_Website.ViewModels
{
	public class InstructorViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "*")]
		[MaxLength(30, ErrorMessage = "Name must be less than 29 letters"), MinLength(2, ErrorMessage = "Name must be greater than 2 letters.")]
		[UniqueInstructor]
		public string? Name { get; set; }
		[Required(ErrorMessage = "*")]
		[RegularExpression("(Alex|Cairo|Tanta|Santa)")]
		public string? Address { get; set; }
		[Required(ErrorMessage = "*")]
		[RegularExpression("01[0125][0-9]{8}", ErrorMessage = "Enter Valid Phone Number.")]
		[UniqueInstructorPhoneNumber]
		public string? PhoneNumber { get; set; }

		[Display(Name = "Image")]
        [Required(ErrorMessage = "*")]
        public IFormFile? File { get; set; }
		public string? ImageUrl { get; set; }
		[DisplayName("Department")]
		[Required(ErrorMessage = "*")]
		public int? dept_id { get; set; }
		[DisplayName("Course")]
		[Required(ErrorMessage = "*")]
		public int? crs_id { get; set; }
	}
}
