using DataAccessLayer.Entities;
namespace DataAccessLayer.Repositories.DepartmentRepo
{
    public interface IDepartmentRepository
	{
		Task<List<Department>>  GetDepartmentsAsync();

		Task<Department> GetDepartmentAsync(int? id);

		Task AddDepartmentAsync(Department department); 

		Task UpdateDepartmentAsync(Department department);

		Task DeleteDepartmentAsync(int id);

		Task<List<Department>> SearchDepartmentAsync(string SearchString);
		Task<int> SaveChangesAsync();
	}
}
