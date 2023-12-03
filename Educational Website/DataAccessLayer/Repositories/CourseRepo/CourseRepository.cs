using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Repositories.CourseRepo
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext context;
        public CourseRepository(ApplicationDbContext db)
        {
            context = db;
        }

        public async Task<List<Course>> GetCoursePerDepartmentAsync(int deptID)
        {
            var courses = await context.Courses.Where(c => c.dept_id == deptID).ToListAsync();
            return courses;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            var courses = await context.Courses.OrderBy(c => c.Name).ToListAsync();
            return courses;
        }

        public async Task<Course> GetCourseAsync(int? id)
        {
            var course = await context.Courses.FirstOrDefaultAsync(u => u.Id == id);
            return course!;
        }

        public async Task AddCourseAsync(Course course)
        {
            await context.Courses.AddAsync(course);
            context.SaveChanges();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            var oldCourse = await context.Courses.FirstOrDefaultAsync(c => c.Id == course.Id);
            if (oldCourse != null)
            {
                oldCourse.Id = course.Id;
                oldCourse.Name = course.Name;
                oldCourse.Grade = course.Grade;
                oldCourse.MinDegree = course.MinDegree;
                oldCourse.dept_id = course.dept_id;
            }
            context.SaveChanges();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course is not null)
                context.Courses.Remove(course);
            context.SaveChanges();
        }

        public async Task<List<Course>> SearchCourseAsync(string SearchString)
        {
            var item = await context.Courses.Include(x => x.department).Where(x => x.Name!.StartsWith(SearchString)).ToListAsync();
            return item;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
