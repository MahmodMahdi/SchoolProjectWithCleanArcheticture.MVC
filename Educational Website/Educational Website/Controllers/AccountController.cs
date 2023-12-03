using BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Authentication;
using Microsoft.AspNetCore.Session;

namespace Educational_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUserVM)
        {
            if (ModelState.IsValid)
            {
                // Mapping from VM to model
                ApplicationUser userModel = new ApplicationUser();
                userModel.FirstName = newUserVM.FirstName;
                userModel.LastName = newUserVM.LastName;
                userModel.UserName = newUserVM.Email;
                userModel.BirthDate = newUserVM.BirthDate;
                userModel.Gender = newUserVM.Gender;
                userModel.Address = newUserVM.Address;
                userModel.Role = "User";
                userModel.PhoneNumber = newUserVM.PhoneNumber;
                userModel.Email = newUserVM.Email;
                userModel.PasswordHash = newUserVM.Password;

                // save in db
                IdentityResult result = await _userManager.CreateAsync(userModel, newUserVM.Password!);
                if (result.Succeeded)
                {
                    // create cookies
                    await _signInManager.SignInAsync(userModel, false); // Store ID, Name, Roles // false as remember me
                    return RedirectToAction("Account", "Login");
                }
                else
                {
                    foreach (var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password", errorItem.Description);
                    }
                }
            }
            return View(newUserVM);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel loginUser)
        {
            if (ModelState.IsValid)
            {
                //check db
                ApplicationUser? userModel = await _userManager.FindByEmailAsync(loginUser.Email);
                if (userModel != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(userModel, loginUser.Password);
                    if (found == true)
                    {
                        //cookie
                        await _signInManager.SignInAsync(userModel, loginUser.RememberMe);
                        return RedirectToAction("Index", "Department");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User name or Password Wrong");
                    }
                }
                //ModelState.AddModelError("", "User name or Password Wrong");
            }
            return View(loginUser);
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin(RegisterUserViewModel newUserVM)
        {
            if (ModelState.IsValid)
            {
                // Mapping from VM to model
                ApplicationUser userModel = new ApplicationUser();
                userModel.FirstName = newUserVM.FirstName;
                userModel.LastName = newUserVM.LastName;
                userModel.UserName = newUserVM.Email;
                userModel.BirthDate = newUserVM.BirthDate;
                userModel.Gender = newUserVM.Gender;
                userModel.Address = newUserVM.Address;
                userModel.PhoneNumber = newUserVM.PhoneNumber;
                userModel.Role = "Admin";
                userModel.Email = newUserVM.Email;
                userModel.PasswordHash = newUserVM.Password;
                // save in db
                IdentityResult result = await _userManager.CreateAsync(userModel, newUserVM.Password!);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userModel, "Admin");
                    // create cookies
                    await _signInManager.SignInAsync(userModel, false); // Store ID, Name, Roles // false as remember me
                    return RedirectToAction("Index", "Department");
                }
                else
                {
                    foreach (var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password", errorItem.Description);
                    }
                }
            }
            return View(newUserVM);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddInstructor()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddInstructor(RegisterUserViewModel newUserVM)
        {
            if (ModelState.IsValid)
            {
                // Mapping from VM to model
                ApplicationUser userModel = new ApplicationUser();
                userModel.FirstName = newUserVM.FirstName;
                userModel.LastName = newUserVM.LastName;
                userModel.UserName = newUserVM.Email;
                userModel.BirthDate = newUserVM.BirthDate;
                userModel.Gender = newUserVM.Gender;
                userModel.Address = newUserVM.Address;
                userModel.Role = "Instructor";
                userModel.PhoneNumber = newUserVM.PhoneNumber;
                userModel.Email = newUserVM.Email;
                userModel.PasswordHash = newUserVM.Password;
                // save in db
                IdentityResult result = await _userManager.CreateAsync(userModel, newUserVM.Password!);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userModel, "Instructor");
                    // create cookies
                    await _signInManager.SignInAsync(userModel, false); // Store ID, Name, Roles // false as remember me
                    return RedirectToAction("Index", "Instructor");
                }
                else
                {
                    foreach (var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password", errorItem.Description);
                    }
                }
            }
            return View(newUserVM);
        }
    }
}
