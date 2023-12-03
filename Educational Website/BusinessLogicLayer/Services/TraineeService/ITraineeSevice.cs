using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
namespace BusinessLogicLayer.Services.TraineeService
{
    public interface ITraineeService
	{
        Task<List<Trainee>> GetAllAsync();

        Task<Trainee> GetTraineeAsync(int id);

        Task AddTraineeAsync(TraineeViewModel traineeVM);

        Task UpdateTraineeAsync(TraineeViewModel traineeVM);

        Task DeleteTraineeAsync(int id);

        Task<List<Trainee>> SearchTraineeAsync(string SearchString);
    }
}
