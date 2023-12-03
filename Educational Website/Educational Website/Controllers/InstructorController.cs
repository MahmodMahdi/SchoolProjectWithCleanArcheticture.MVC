using BusinessLogicLayer.Services.CourseService;
using BusinessLogicLayer.Services.DepartmentService;
using BusinessLogicLayer.Services.InstructorService;
using Educational_Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Educational_Website.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;
        private readonly IDepartmentService _departmentService;
        private readonly ICourseService _courseService;
        private readonly IWebHostEnvironment _webHost;

        public InstructorController(IInstructorService instructorService,
            IDepartmentService departmentService,
            ICourseService courseService,
            IWebHostEnvironment webHost)
        {
            _instructorService = instructorService;
            _departmentService = departmentService;
            _courseService = courseService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            var GetAllInstructor = await _instructorService.GetAllAsync();
            return View(GetAllInstructor);
        }
        public async Task<IActionResult> Details(int id)
        {
            var instructor = await _instructorService.GetInstructorAsync(id);
            InstructorViewModel instructorVM = new()
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Address = instructor.Address,
                PhoneNumber = instructor.PhoneNumber,
                ImageUrl = instructor.ImageUrl,
                crs_id = instructor.crs_id,
                dept_id = instructor.dept_id,
            };
            ViewBag.DeptName = await _departmentService.GetDepartmentAsync(instructorVM.dept_id);
            ViewBag.CourseName = await _courseService.GetCourseAsync(instructorVM.crs_id);
            return View(instructorVM);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            await Helper();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(InstructorViewModel instructorVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await Helper();
                    return View();
                }

                instructorVM.ImageUrl = "Image/InstructorPhotos/" + Guid.NewGuid().ToString();
                instructorVM.ImageUrl += instructorVM.File!.FileName;
                string fullPath = Path.Combine(_webHost.WebRootPath, instructorVM.ImageUrl);
                using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                {
                    instructorVM.File?.CopyTo(fs);
                };

                await _instructorService.AddInstructorAsync(instructorVM);
                TempData["successMessageForAddingInstructor"] = "Instructor Added successfully!";
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
            string oldFileName = (await _instructorService.GetInstructorAsync(id)).ImageUrl!;
            string fullOldPath = Path.Combine(_webHost.WebRootPath, oldFileName);
            System.IO.File.Delete(fullOldPath);
            await _instructorService.DeleteInstructorAsync(id);
            TempData["successMessageForDeleteInstructor"] = "Instructor deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _instructorService.GetInstructorAsync(id);
            InstructorViewModel InstructorVM = new()
            {
                Name = instructor.Name,
                Address = instructor.Address,
                PhoneNumber = instructor.PhoneNumber,
                ImageUrl = instructor.ImageUrl,
                dept_id = instructor.dept_id,
                crs_id = instructor.crs_id,
            };
            await Helper();
            return View(InstructorVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(InstructorViewModel instructorVM)
        {
            if (!ModelState.IsValid)
            {
                await Helper();
                return View();
            }
            var oldUrl = instructorVM.ImageUrl;
            instructorVM.ImageUrl = "Image/InstructorPhotos/";
            instructorVM.ImageUrl = instructorVM.File?.FileName;
            string fullPath = Path.Combine(_webHost.WebRootPath, instructorVM.ImageUrl!);
            if (instructorVM.ImageUrl != oldUrl)
            {
                instructorVM.ImageUrl = "Image/InstructorPhotos/" + Guid.NewGuid().ToString();
                instructorVM.ImageUrl += instructorVM.File?.FileName;
                string fullPath2 = Path.Combine(_webHost.WebRootPath, instructorVM.ImageUrl!);
                string oldFileName = (await _instructorService.GetInstructorAsync(instructorVM.Id)).ImageUrl!;
                string fullOldPath = Path.Combine(_webHost.WebRootPath, oldFileName);
                if (fullPath2 != fullOldPath)
                {
                    using (FileStream fs = new FileStream(fullPath2, FileMode.Create))
                    {
                        System.IO.File.Delete(fullOldPath);
                        instructorVM.File?.CopyToAsync(fs);
                    }
                }
            }
            await _instructorService.UpdateInstructorAsync(instructorVM);
            TempData["successMessageForEditInstructor"] = "Instructor Updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Search(string searchString)
        {
            var Search = await _instructorService.SearchInstructorAsync(searchString);
            return View(Search);
        }
        private async Task Helper()
        {
            ViewBag.DeptName = await _departmentService.GetDepartmentsAsync();
            ViewBag.CourseName = await _courseService.GetAllAsync();
        }
    }
}
