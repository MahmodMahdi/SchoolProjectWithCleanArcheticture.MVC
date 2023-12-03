using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
namespace BusinessLogicLayer.Services.InstructorService
{
    public interface IInstructorService
	{
		Task<List<Instructor>> GetAllAsync();

		Task<Instructor> GetInstructorAsync(int id);

		Task AddInstructorAsync(InstructorViewModel instructorVM);

		Task UpdateInstructorAsync(InstructorViewModel instructorVM);

		Task DeleteInstructorAsync(int id);

		Task<List<Instructor>> SearchInstructorAsync(string SearchString);
	}
}
