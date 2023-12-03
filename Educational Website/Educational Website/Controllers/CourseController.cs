using BusinessLogicLayer.Services.CourseService;
using BusinessLogicLayer.Services.DepartmentService;
using Educational_Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Educational_Website.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IDepartmentService _departmentService;
        public CourseController(ICourseService courseService, IDepartmentService departmentService)
        {
            _courseService = courseService;
            _departmentService = departmentService;
        }
        public IActionResult CheckGradeAsync(int MinDegree, int Grade)
        {
            if (MinDegree <= Grade)
            {
                return Json(true);
            }
            return Json(false);
        }
        public async Task<IActionResult> GetCoursePerDepartment(int deptID)
        {
            var courses = await _courseService.GetCoursePerDepartmentAsync(deptID);
            return Json(courses);
        }
        public async Task<IActionResult> Index()
        {
            var GetAllCourses = await _courseService.GetAllAsync();
            return View(GetAllCourses);
        }
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            CourseViewModel courseVM = new()
            {
                Id = course.Id,
                Name = course.Name!,
                Grade = course.Grade,
                MinDegree = course.MinDegree,
                dept_id = course.dept_id
            };
            ViewBag.DeptName = await _departmentService.GetDepartmentAsync(course.dept_id);
            return View(courseVM);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            ViewBag.DeptDropDownList = await _departmentService.GetDepartmentsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(CourseViewModel courseVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.DeptDropDownList = await _departmentService.GetDepartmentsAsync();
                    //TempData["errorMessage"] = "Model state is invalid";
                    return View();
                }
                await _courseService.AddCourseAsync(courseVM);
                TempData["successMessageForAddingCourse"] = "Course Added successfully!";
            }
            catch
            {
                //TempData["errorMessage"] = "Model state is invalid";
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            TempData["successMessageForDeleteCourse"] = "Course deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            ViewBag.DeptDropDownList = await _departmentService.GetDepartmentsAsync();
            CourseViewModel courseVM = new()
            {
                Name = course.Name!,
                Grade = course.Grade,
                MinDegree = course.MinDegree,
                dept_id = course.dept_id,
            };
            return View(courseVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(CourseViewModel courseVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DeptDropDownList = await _departmentService.GetDepartmentsAsync();
                return View();
            }
            await _courseService.UpdateCourseAsync(courseVM);
            TempData["successMessageForEditCourse"] = "Course Updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var Search = await _courseService.SearchCourseAsync(searchString);
            ViewBag.Search = searchString;
            return View(Search);
        }
    }
}
