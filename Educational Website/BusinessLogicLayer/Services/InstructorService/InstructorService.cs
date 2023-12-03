using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.InstructorRepo;
using Educational_Website.ViewModels;
namespace BusinessLogicLayer.Services.InstructorService
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorService(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<List<Instructor>> GetAllAsync()
        {
            return await _instructorRepository.GetAllAsync();
        }

        public async Task<Instructor> GetInstructorAsync(int id)
        {
            return await _instructorRepository.GetInstructorAsync(id);
        }

        public async Task AddInstructorAsync(InstructorViewModel instructorVM)
        {
            Instructor instructor = new Instructor()
            {
                Name = instructorVM.Name!,
                Address = instructorVM.Address!,
                PhoneNumber = instructorVM.PhoneNumber,
                dept_id = instructorVM.dept_id,
                crs_id = instructorVM.crs_id,
                ImageUrl = instructorVM.ImageUrl,
            };
            await _instructorRepository.AddInstructorAsync(instructor);
            await _instructorRepository.SaveChangesAsync();
        }

        public async Task UpdateInstructorAsync(InstructorViewModel instructorVM)
        {
            var instructor = await _instructorRepository.GetInstructorAsync(instructorVM.Id);
            instructor.Name = instructorVM.Name!;
            instructor.Address = instructorVM.Address!;
            instructor.PhoneNumber = instructorVM.PhoneNumber;
            instructor.dept_id = instructorVM.dept_id;
            instructor.crs_id = instructorVM.crs_id;
            instructor.ImageUrl = instructorVM.ImageUrl;
            await _instructorRepository.UpdateInstructorAsync(instructor);
            await _instructorRepository.SaveChangesAsync();
        }

        public async Task DeleteInstructorAsync(int id)
        {
            await _instructorRepository.DeleteInstructorAsync(id);
            await _instructorRepository.SaveChangesAsync();
        }

        public async Task<List<Instructor>> SearchInstructorAsync(string SearchString)
        {
            return await _instructorRepository.SearchInstructorAsync(SearchString);
        }
    }
}
