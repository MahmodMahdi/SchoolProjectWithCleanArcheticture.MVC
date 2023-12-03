using DataAccessLayer.Entities;
namespace DataAccessLayer.Repositories.TraineeRepo
{
    public interface ITraineeRepository
	{
		Task<List<Trainee>> GetAllAsync();

		Task<Trainee> GetTraineeAsync(int id);

		Task AddTraineeAsync(Trainee trainee);

		Task UpdateTraineeAsync(Trainee trainee);

		Task DeleteTraineeAsync(int id);

		Task<List<Trainee>> SearchTraineeAsync(string SearchString);

		Task<int> SaveChangesAsync();
	}
}
