namespace DataAccessLayer.Entities
{
    public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? DeptManager { get; set; }
		public virtual List<Instructor> Instructors { get; set; } = new List<Instructor>();
		public virtual List<Trainee> Trainees { get; set; } = new List<Trainee>();
		public virtual List<Course> Courses { get; set; } = new List<Course>();
	}
}
