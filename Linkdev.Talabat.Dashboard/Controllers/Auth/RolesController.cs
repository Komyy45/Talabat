using AutoMapper;
using Linkdev.Talabat.Dashboard.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Linkdev.Talabat.Dashboard.Controllers.Auth
{
	[Authorize(AuthenticationSchemes = "Identity.Application")]
	public class RolesController(RoleManager<IdentityRole> roleManager, IMapper mapper) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var roles = await roleManager.Roles.ToListAsync();
			var mappedRoles = mapper.Map<IEnumerable<RoleViewModel>>(roles);
			return View(mappedRoles);
		}

		[HttpPost]
		public async Task<IActionResult> Create(RoleViewModel model)
		{
		 	var mappedRole = mapper.Map<IdentityRole>(model);


			mappedRole.Id = Guid.NewGuid().ToString();
			await roleManager.CreateAsync(mappedRole);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			var role = await roleManager.FindByIdAsync(id);
			if (role is null) return NotFound();

			await roleManager.DeleteAsync(role);

			return RedirectToAction("Index");
		}
	}
}
