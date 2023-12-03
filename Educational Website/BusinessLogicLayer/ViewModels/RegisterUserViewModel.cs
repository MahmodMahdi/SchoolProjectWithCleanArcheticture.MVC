using System.ComponentModel.DataAnnotations;
namespace BusinessLogicLayer.ViewModels
{
	public class RegisterUserViewModel
	{
		[Required(ErrorMessage = "*")]
		[MaxLength(30, ErrorMessage = "Name must be less than 29 letters"), MinLength(2, ErrorMessage = "Name must be greater than 2 letters.")]
		public string? FirstName { get; set; }
		[Required(ErrorMessage = "*")]
		[MaxLength(30, ErrorMessage = "Name must be less than 29 letters"), MinLength(2, ErrorMessage = "Name must be greater than 2 letters.")]
		public string? LastName { get; set; }
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "dd/MM/yyyy", ApplyFormatInEditMode = true)]
		public DateTime? BirthDate { get; set; }
		public string? Gender { get; set; }
		[Required(ErrorMessage = "*")]
		public string? Address { get; set; }
		[Required(ErrorMessage = "*")]
		[RegularExpression("01[0125][0-9]{8}", ErrorMessage = "Enter Valid Phone Number.")]
		public string? PhoneNumber { get; set; }
		[Required(ErrorMessage = "*")]
		[DataType(DataType.EmailAddress)]
		[RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{3}", ErrorMessage = "Enter valid Email")]

		public string? Email { get; set; }
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string? ConfirmPassword { get; set; }
	}
}
