using DataAccessLayer.Entities;
using Educational_Website.ViewModels;
namespace BusinessLogicLayer.Services.DepartmentService
{
    public interface IDepartmentService
	{
		Task<List<Department>> GetDepartmentsAsync();

		Task<Department> GetDepartmentAsync(int? id);

		Task AddDepartmentAsync(DepartmentViewModel departmentVM);

		Task UpdateDepartmentAsync(DepartmentViewModel departmentVM);

		Task DeleteDepartmentAsync(int id);

		Task<List<Department>> SearchDepartmentAsync(string SearchString);
	}
}
