using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
namespace BusinessLogicLayer.Services.CourseService
{
    public interface ICourseService
    {
        Task<List<Course>> GetCoursePerDepartmentAsync(int deptID);

        Task<List<Course>> GetAllAsync();

        Task<Course> GetCourseAsync(int? id);

        Task AddCourseAsync(CourseViewModel courseVM);

        Task UpdateCourseAsync(CourseViewModel courseVM);

        Task DeleteCourseAsync(int id);

        Task<List<Course>> SearchCourseAsync(string SearchString);
    }
}
