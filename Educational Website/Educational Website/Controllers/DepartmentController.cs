using BusinessLogicLayer.Services.DepartmentService;
using BusinessLogicLayer.Services.TraineeService;
using Educational_Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Educational_Website.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ITraineeService _traineeService;

        public DepartmentController(IDepartmentService departmentService,
                                    ITraineeService traineeService)
        {
            _departmentService = departmentService;
            _traineeService = traineeService;
        }

        public async Task<IActionResult> Index()
        {
            var GetAllDepartments = await _departmentService.GetDepartmentsAsync();
            return View(GetAllDepartments);
        }
        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetDepartmentAsync(id);
            DepartmentViewModel DepartmentVM = new()
            {
                Id = department.Id,
                Name = department.Name,
                DeptManager = department.DeptManager
            };
            return View(DepartmentVM);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(DepartmentViewModel departmentVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _departmentService.AddDepartmentAsync(departmentVM);
                TempData["successMessageForAddingDepartment"] = "Department Added successfully!";
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            TempData["successMessageForDeleteDepartment"] = "Department deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentService.GetDepartmentAsync(id);
            DepartmentViewModel departmentVM = new()
            {
                Id = department.Id,
                Name = department.Name,
                DeptManager = department.DeptManager
            };
            return View(departmentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _departmentService.UpdateDepartmentAsync(departmentVM);
            TempData["successMessageForEditDepartment"] = "Department Updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Search(string searchString)
        {
            var Search = await _departmentService.SearchDepartmentAsync(searchString);
            return View(Search);
        }
    }
}
