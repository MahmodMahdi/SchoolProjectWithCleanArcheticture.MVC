using BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Website.Controllers
{
	[Authorize(Roles = "Admin")]
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RoleController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult New()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> New(RoleViewModel roleViewModel)
		{
			if (ModelState.IsValid == true)
			{
				IdentityRole roleModel = new IdentityRole();
				roleModel.Name = roleViewModel.RoleName;
				IdentityResult result = await _roleManager.CreateAsync(roleModel);
				if (result.Succeeded)
				{
                    TempData["successMessageForAddingRole"] = "Role Added successfully!";
                    return RedirectToAction("Index");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}
			}
			return View();
		}
	}
	
}
