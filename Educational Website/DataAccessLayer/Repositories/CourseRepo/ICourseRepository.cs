using DataAccessLayer.Entities;
namespace DataAccessLayer.Repositories.CourseRepo
{
	public interface ICourseRepository
	{
		Task<List<Course>> GetCoursePerDepartmentAsync(int deptID);

		Task<List<Course>> GetAllAsync();

		Task<Course> GetCourseAsync(int? id);

		Task AddCourseAsync(Course course);

		Task UpdateCourseAsync(Course course);

		Task DeleteCourseAsync(int id);

		Task<List<Course>> SearchCourseAsync(string SearchString);

		Task<int> SaveChangesAsync();
	}
}
