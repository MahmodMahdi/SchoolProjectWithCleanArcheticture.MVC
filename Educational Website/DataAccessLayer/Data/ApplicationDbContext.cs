using DataAccessLayer.Authentication;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext() : base() { }
		//inject
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Trainee> Trainees { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<CourseResult> CourseResult { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			builder.Entity<ApplicationUser>().ToTable("Users", "security");
			builder.Entity<IdentityRole>().ToTable("Roles", "security");
			builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
			builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
			builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
			builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
			builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
        }
    }
}
