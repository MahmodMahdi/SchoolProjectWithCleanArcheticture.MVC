using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.DepartmentRepo;
using Educational_Website.ViewModels;
namespace BusinessLogicLayer.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _departmentRepository.GetDepartmentsAsync();
        }

        public async Task<Department> GetDepartmentAsync(int? id)
        {
            return await _departmentRepository.GetDepartmentAsync(id);
        }

        public async Task AddDepartmentAsync(DepartmentViewModel departmentVM)
        {
            Department department = new()
            {
                Name = departmentVM.Name,
                DeptManager = departmentVM.DeptManager
            };
            await _departmentRepository.AddDepartmentAsync(department);
            await _departmentRepository.SaveChangesAsync();
        }

        public async Task UpdateDepartmentAsync(DepartmentViewModel departmentVM)
        {
            var department = await _departmentRepository.GetDepartmentAsync(departmentVM.Id);
            department.Id = departmentVM.Id;
            department.Name = departmentVM.Name;
            department.DeptManager = departmentVM.DeptManager;
            await _departmentRepository.UpdateDepartmentAsync(department);
            await _departmentRepository.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
            await _departmentRepository.SaveChangesAsync();
        }

        public async Task<List<Department>> SearchDepartmentAsync(string SearchString)
        {
            return await _departmentRepository.SearchDepartmentAsync(SearchString);
        }

    }
}
