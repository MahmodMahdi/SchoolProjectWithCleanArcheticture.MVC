using DataAccessLayer.Entities;
namespace DataAccessLayer.Repositories.TraineeCoursesResultsRepo
{
    public interface ICourseResultRepository
	{
		Task<List<CourseResult>> GetResultsByIdAsync(int id);

		Task<List<CourseResult>> GetAllAsync();

		Task<CourseResult> GetCourseResultAsync(int id);

		Task AddCourseResultAsync(CourseResult crsResult);

		Task UpdateCourseResultAsync(CourseResult crsResult);

		Task DeleteCourseResultAsync(int id);

		Task<List<CourseResult>> SearchCourseResultAsync(string SearchString);

		Task<int> SaveChangesAsync();
	}
}
