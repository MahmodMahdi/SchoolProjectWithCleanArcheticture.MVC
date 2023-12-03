using DataAccessLayer.Entities;
namespace DataAccessLayer.Repositories.InstructorRepo
{
    public interface IInstructorRepository
	{
		Task<List<Instructor>> GetAllAsync();

		Task<Instructor> GetInstructorAsync(int id);

		Task AddInstructorAsync(Instructor instructor);
		
		Task UpdateInstructorAsync(Instructor instructor);
		
		Task DeleteInstructorAsync(int id);

		Task<List<Instructor>> SearchInstructorAsync(string SearchString);

		Task<int> SaveChangesAsync();
	}
}
