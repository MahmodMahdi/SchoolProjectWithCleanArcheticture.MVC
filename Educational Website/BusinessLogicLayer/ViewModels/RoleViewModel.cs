using System.ComponentModel.DataAnnotations;
namespace BusinessLogicLayer.ViewModels
{
	public class RoleViewModel
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
