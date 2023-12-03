using BusinessLogicLayer.Services.DepartmentService;
using BusinessLogicLayer.Services.TraineeCoursesResultsService;
using BusinessLogicLayer.Services.TraineeService;
using Educational_Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Website.Controllers
{
    public class TraineeController : Controller
    {
        private readonly ITraineeService _traineeService;
        private readonly IDepartmentService _departmentService;
        private readonly ICourseResultService _traineeCoursesResultsService;
        private readonly IWebHostEnvironment _webHost;
        public TraineeController(ITraineeService traineeService,
            IDepartmentService departmentService,
            ICourseResultService traineeCoursesResultsService,
            IWebHostEnvironment webHost)
        {
            _traineeService = traineeService;
            _departmentService = departmentService;
            _traineeCoursesResultsService = traineeCoursesResultsService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            var ItemsList = await _traineeService.GetAllAsync();
            return View(ItemsList);
        }
        public async Task<IActionResult> Details(int id)
        {
            var trainee = await _traineeService.GetTraineeAsync(id);
            TraineeViewModel TraineeVM = new()
            {
                Id = trainee.Id,
                Name = trainee.Name!,
                Address = trainee.Address,
                PhoneNumber = trainee.PhoneNumber,
                Level = trainee.Level,
                ImageUrl = trainee.ImageUrl,
                dept_id = trainee.dept_id
            };
            ViewBag.DeptName = await _departmentService.GetDepartmentAsync(trainee.dept_id);
            return View(TraineeVM);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Add()
        {
            ViewBag.DeptDropDownList = await _departmentService.GetDepartmentsAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Add(TraineeViewModel traineeVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.DeptDropDownList = await _departmentService.GetDepartmentsAsync();
                    return View();
                }
                #region Upload Image 
                traineeVM.ImageUrl = "Image/TraineePhotos/" + Guid.NewGuid().ToString();
                traineeVM.ImageUrl += traineeVM.File!.FileName;
                string serverFolder = Path.Combine(_webHost.WebRootPath, traineeVM.ImageUrl);
                //await traineevm.File.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                using (FileStream fs = new FileStream(serverFolder, FileMode.Create))
                {
                    traineeVM.File?.CopyTo(fs);
                };
                #endregion
                await _traineeService.AddTraineeAsync(traineeVM);
                TempData["successMessageForAddingTrainee"] = "Trainee Added successfully!";
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Delete(int id)
        {
            string oldFileName = (await _traineeService.GetTraineeAsync(id)).ImageUrl!;
            string fullOldPath = Path.Combine(_webHost.WebRootPath, oldFileName);
            System.IO.File.Delete(fullOldPath);
            await _traineeService.DeleteTraineeAsync(id);
            TempData["successMessageForDeleteTrainee"] = "Trainee deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Edit(int id)
        {
            var traineeToEdit = await _traineeService.GetTraineeAsync(id);

            TraineeViewModel trainee = new()
            {
                Id = traineeToEdit.Id,
                Name = traineeToEdit.Name!,
                Address = traineeToEdit.Address,
                PhoneNumber = traineeToEdit.PhoneNumber,
                ImageUrl = traineeToEdit.ImageUrl,
                Level = traineeToEdit.Level,
                dept_id = traineeToEdit.dept_id
            };
            ViewBag.DeptDropDownList = await _departmentService.GetDepartmentsAsync();
            return View(trainee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Edit(TraineeViewModel traineeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DeptDropDownList = await _departmentService.GetDepartmentsAsync();
                return View();
            }
            #region Update Image
            var oldUrl = traineeVM.ImageUrl;
            traineeVM.ImageUrl = "Image/TraineePhotos/";
            traineeVM.ImageUrl = traineeVM.File?.FileName;
            string fullPath = Path.Combine(_webHost.WebRootPath, traineeVM.ImageUrl!);
            if (traineeVM.ImageUrl != oldUrl)
            {
                traineeVM.ImageUrl = "Image/TraineePhotos/" + Guid.NewGuid().ToString();
                traineeVM.ImageUrl += traineeVM.File?.FileName;
                string fullPath2 = Path.Combine(_webHost.WebRootPath, traineeVM.ImageUrl!);
                string oldFileName = (await _traineeService.GetTraineeAsync(traineeVM.Id)).ImageUrl!;
                string fullOldPath = Path.Combine(_webHost.WebRootPath, oldFileName);
                if (fullPath2 != fullOldPath)
                {
                    using (FileStream fs = new FileStream(fullPath2, FileMode.Create))
                    {
                        System.IO.File.Delete(fullOldPath);
                        traineeVM.File?.CopyTo(fs);
                    }
                }
            }
            #endregion
            await _traineeService.UpdateTraineeAsync(traineeVM);
            TempData["successMessageForEditTrainee"] = "Trainee Updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Search(string searchString)
        {
            var Search = await _traineeService.SearchTraineeAsync(searchString);
            return View(Search);
        }
    }
}
