using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.DepartmentRepo
{
    public class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDbContext context;
		public DepartmentRepository(ApplicationDbContext db)
		{
			context = db;
		}

		public async Task<List<Department>> GetDepartmentsAsync()
		{
			var departments =await context.Departments.OrderBy(d=>d.Name).ToListAsync();
			return departments;
		}

		public async Task<Department> GetDepartmentAsync(int? id)
		{
			var department =await context.Departments.FirstOrDefaultAsync(u => u.Id == id);
			return department!;
		}

		public async Task AddDepartmentAsync(Department department)
		{
			await context.Departments.AddAsync(department);
			await context.SaveChangesAsync();
		}

		public async Task UpdateDepartmentAsync(Department department)
		{
			var oldDepartment =await context.Departments.FirstOrDefaultAsync(c => c.Id == department.Id);
			if (oldDepartment != null)
			{
				oldDepartment.Name = department.Name;
				oldDepartment.DeptManager = department.DeptManager;
			}
			await context.SaveChangesAsync();
		}

		public async Task DeleteDepartmentAsync(int id)
		{
			var department =await context.Departments.FirstOrDefaultAsync(c => c.Id == id);
			if (department is not null)
				 context.Departments.Remove(department);
			context.SaveChanges();
		}

		public async Task<List<Department>> SearchDepartmentAsync(string SearchString)
		{
			var item =await context.Departments.Where(x => x.Name.StartsWith(SearchString)).ToListAsync();
			return item;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await context.SaveChangesAsync();
		}
	}
}
