using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccessLayer.Entities
{
    public class Course
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int Grade { get; set; }
		public int MinDegree { get; set; }
		[ForeignKey(nameof(department))]
		public int? dept_id { get; set; }
		public virtual Department? department { get; set; }
		public virtual List<CourseResult>? courseResult { get; set; }
		public virtual List<Instructor>? instructors { get; set; }
	}
}
