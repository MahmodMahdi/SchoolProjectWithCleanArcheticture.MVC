using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
namespace BusinessLogicLayer.Services.TraineeCoursesResultsService
{
    public interface ICourseResultService
    {
        Task<List<CourseResult>> GetResultsByIdAsync(int id);

        Task<List<CourseResult>> GetAllAsync();

        Task<CourseResult> GetCourseResultAsync(int id);

        Task AddCourseResultAsync(CourseResultViewModel crsResultVM);

        Task UpdateCourseResultAsync(CourseResultViewModel crsResultVM);

        Task DeleteCourseResultAsync(int id);

        Task<List<CourseResult>> SearchCourseResultAsync(string SearchString);
    }
}
