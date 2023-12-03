using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BusinessLogicLayer.Validations;
namespace Educational_Website.ViewModels
{
	public class TraineeViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "*")]
		[MaxLength(30, ErrorMessage = "Name must be less than 29 letters"), MinLength(2, ErrorMessage = "Name must be greater than 2 letters.")]
		[UniqueTrainee]
		public string Name { get; set; } = string.Empty;
		[Required(ErrorMessage = "*")]
		[RegularExpression("(Alex|Cairo|Tanta|Santa|Menofia)")]
		public string? Address { get; set; }
		[Required(ErrorMessage = "*")]
		[RegularExpression("01[0125][0-9]{8}", ErrorMessage = "Enter Valid Phone Number.")]
		[UniqueTraineePhoneNumber]
		public string? PhoneNumber { get; set; }
		[DisplayName("Image")]
        [Required(ErrorMessage = "*")]
        public IFormFile? File { get; set; }
		public string? ImageUrl { get; set; }
		[Required(ErrorMessage = "*")]
		[Range(1, 4)]
		public int Level { get; set; }
		[DisplayName("Department")]
		[Required(ErrorMessage = "*")]
		public int? dept_id { get; set; }
	}
}
