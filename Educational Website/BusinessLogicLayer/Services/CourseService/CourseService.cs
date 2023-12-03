using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.CourseRepo;
using Educational_Website.ViewModels;
namespace BusinessLogicLayer.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<Course>> GetCoursePerDepartmentAsync(int deptID)
        {
            return await _courseRepository.GetCoursePerDepartmentAsync(deptID);
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task<Course> GetCourseAsync(int? id)
        {
            return await _courseRepository.GetCourseAsync(id);
        }

        public async Task AddCourseAsync(CourseViewModel courseVM)
        {
            Course course = new()
            {
                Name = courseVM.Name!,
                Grade = courseVM.Grade,
                MinDegree = courseVM.MinDegree,
                dept_id = courseVM.dept_id
            };
            await _courseRepository.AddCourseAsync(course);
            await _courseRepository.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(CourseViewModel courseVM)
        {
            var course = await _courseRepository.GetCourseAsync(courseVM.Id);
            course.Id = courseVM.Id;
            course.Name = courseVM.Name!;
            course.Grade = courseVM.Grade;
            course.MinDegree = courseVM.MinDegree;
            course.dept_id = courseVM.dept_id;
            await _courseRepository.UpdateCourseAsync(course);
            await _courseRepository.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepository.DeleteCourseAsync(id);
            await _courseRepository.SaveChangesAsync();
        }

        public async Task<List<Course>> SearchCourseAsync(string SearchString)
        {
            return await _courseRepository.SearchCourseAsync(SearchString);
        }
    }
}
