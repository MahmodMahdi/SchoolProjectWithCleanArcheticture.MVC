using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccessLayer.Entities
{
    public class Instructor
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string? PhoneNumber { get; set; }
		public string? ImageUrl { get; set; }
		[ForeignKey(nameof(department))]
		public int? dept_id { get; set; }
		[ForeignKey(nameof(course))]
		public int? crs_id { get; set; }
		[NotMapped]
		public IFormFile? File { get; set; }
		public virtual Course? course { get; set; }

		public virtual Department? department { get; set; }
	}
}
