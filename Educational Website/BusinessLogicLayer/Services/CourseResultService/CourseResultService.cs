using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.TraineeCoursesResultsRepo;
using Educational_Website.ViewModels;
namespace BusinessLogicLayer.Services.TraineeCoursesResultsService
{
    public class CourseResultService : ICourseResultService
    {
        private readonly ICourseResultRepository _courseResultRepository;
        public CourseResultService(ICourseResultRepository courseResultRepository)
        {

            _courseResultRepository = courseResultRepository;
        }

        public async Task<List<CourseResult>> GetResultsByIdAsync(int id)
        {
            return await _courseResultRepository.GetResultsByIdAsync(id);
        }

        public async Task<List<CourseResult>> GetAllAsync()
        {
            return await _courseResultRepository.GetAllAsync();
        }

        public async Task<CourseResult> GetCourseResultAsync(int id)
        {
            return await _courseResultRepository.GetCourseResultAsync(id);
        }

        public async Task AddCourseResultAsync(CourseResultViewModel courseResultVM)
        {
            CourseResult courseResult = new()
            {
                trainee_id = courseResultVM.trainee_id,
                crs_id = courseResultVM.crs_id,
                Degree = courseResultVM.Degree,
            };
            await _courseResultRepository.AddCourseResultAsync(courseResult);
            await _courseResultRepository.SaveChangesAsync();
        }

        public async Task UpdateCourseResultAsync(CourseResultViewModel courseResultVM)
        {
            var courseResult = await _courseResultRepository.GetCourseResultAsync(courseResultVM.Id);
            courseResult.trainee_id = courseResultVM.trainee_id;
            courseResult.crs_id = courseResultVM.crs_id;
            courseResult.Degree = courseResultVM.Degree;
            await _courseResultRepository.UpdateCourseResultAsync(courseResult);
            await _courseResultRepository.SaveChangesAsync();
        }

        public async Task DeleteCourseResultAsync(int id)
        {
            await _courseResultRepository.DeleteCourseResultAsync(id);
            await _courseResultRepository.SaveChangesAsync();
        }

        public async Task<List<CourseResult>> SearchCourseResultAsync(string SearchString)
        {
            return await _courseResultRepository.SearchCourseResultAsync(SearchString);
        }

    }
}
