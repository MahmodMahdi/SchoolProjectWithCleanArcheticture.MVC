using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Repositories.TraineeRepo
{
	public class TraineeRepository : ITraineeRepository
	{
		private readonly ApplicationDbContext context;
		public TraineeRepository(ApplicationDbContext db)
		{
			context = db;
		}

		public async Task<List<Trainee>> GetAllAsync()
		{
			var trainees =await context.Trainees.OrderBy(t => t.Name).ToListAsync();
			return trainees;
		}

		public async Task<Trainee> GetTraineeAsync(int id)
		{
			var trainee =await context.Trainees.Include(x => x.department).FirstOrDefaultAsync(u => u.Id == id);
			return trainee!;
		}

		public async Task AddTraineeAsync(Trainee trainee)
		{
			await context.Trainees.AddAsync(trainee);
			await context.SaveChangesAsync();
		}

		public async Task UpdateTraineeAsync(Trainee trainee)
		{
			var oldTrainee =await context.Trainees.FirstOrDefaultAsync(c => c.Id == trainee.Id);
			if (oldTrainee != null)
			{
				oldTrainee.Name = trainee.Name;
				oldTrainee.Address = trainee.Address;
				oldTrainee.Level = trainee.Level;
				oldTrainee.PhoneNumber = trainee.PhoneNumber;
				oldTrainee.ImageUrl = trainee.ImageUrl;
				oldTrainee.dept_id = trainee.dept_id;
			}
			await context.SaveChangesAsync();
		}

		public async Task DeleteTraineeAsync(int id)
		{
			var Trainee =await context.Trainees.FirstOrDefaultAsync(c => c.Id == id);
			if (Trainee is not null)
				context.Trainees.Remove(Trainee);
			await context.SaveChangesAsync();
		}

		public async Task <List<Trainee>> SearchTraineeAsync(string SearchString)
		{
			var item =await context.Trainees.Include(d => d.department).Where(x => x.Name.StartsWith(SearchString)).ToListAsync();
			return item;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await context.SaveChangesAsync();
		}
	}
}
