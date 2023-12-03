using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.TraineeRepo;
using Educational_Website.ViewModels;
namespace BusinessLogicLayer.Services.TraineeService
{
    public class TraineeService : ITraineeService
    {
        private readonly ITraineeRepository _traineeRepository;
        public TraineeService(ITraineeRepository traineeRepository)
        {
            _traineeRepository = traineeRepository;
        }

        public async Task<List<Trainee>> GetAllAsync()
        {
            return await _traineeRepository.GetAllAsync();
        }

        public async Task<Trainee> GetTraineeAsync(int id)
        {
            return await _traineeRepository.GetTraineeAsync(id);
        }

        public async Task AddTraineeAsync(TraineeViewModel traineeVM)
        {
            Trainee trainee = new Trainee()
            {
                Id = traineeVM.Id,
                Name = traineeVM.Name,
                Address = traineeVM.Address!,
                PhoneNumber = traineeVM.PhoneNumber,
                ImageUrl = traineeVM.ImageUrl,
                Level = traineeVM.Level,
                dept_id = traineeVM.dept_id
            };
            await _traineeRepository.AddTraineeAsync(trainee);
            await _traineeRepository.SaveChangesAsync();
        }

        public async Task UpdateTraineeAsync(TraineeViewModel traineeVM)
        {
            var trainee = await _traineeRepository.GetTraineeAsync(traineeVM.Id);
            trainee.Id = traineeVM.Id;
            trainee.Name = traineeVM.Name;
            trainee.Address = traineeVM.Address!;
            trainee.PhoneNumber = traineeVM.PhoneNumber;
            trainee.ImageUrl = traineeVM.ImageUrl;
            trainee.Level = traineeVM.Level;
            trainee.dept_id = traineeVM.dept_id;
            await _traineeRepository.UpdateTraineeAsync(trainee);
            await _traineeRepository.SaveChangesAsync();
        }

        public async Task DeleteTraineeAsync(int id)
        {
            await _traineeRepository.DeleteTraineeAsync(id);
            await _traineeRepository.SaveChangesAsync();
        }

        public async Task<List<Trainee>> SearchTraineeAsync(string SearchString)
        {
            return await _traineeRepository.SearchTraineeAsync(SearchString);
        }
    }
}
