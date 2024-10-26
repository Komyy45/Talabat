using AutoMapper;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Linkdev.Talabat.Dashboard.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Linkdev.Talabat.Dashboard.Controllers.Auth
{
	[Authorize(AuthenticationSchemes = "Identity.Application")]
	public class UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var users = await  userManager.Users.AsNoTracking().ToListAsync();

			var mappedUsers = users.Select(user => new ApplicationUserViewModel()
			{ 
				Id = user.Id,
				DisplayName = user.DisplayName, 
				Email = user.Email!, 
				PhoneNumber = user.PhoneNumber!, 
				UserName = user.UserName!,  
				Roles = userManager.GetRolesAsync(user).Result,  
			} );

			return View(mappedUsers);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			var user = await userManager.FindByIdAsync(id);
			if (user is null) return NotFound();
			
			var mappedUser = mapper.Map<ApplicationUserEditViewModel>(user);

			var roles = await roleManager.Roles.ToListAsync();

			mappedUser.Roles = mapper.Map<IEnumerable<RoleViewModel>>(roles).Select(
				(role) => new RoleViewModel() 
				{ 
					Id = role.Id, 
					Name = role.Name, 
					IsSelected = userManager.IsInRoleAsync(user, role.Name).Result }
				).ToList();

			return View(mappedUser);
		}

		[HttpPost]
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserEditViewModel user)
        {
            if (!ModelState.IsValid) return View(user);

            // Retrieve the existing user from UserManager
            var existingUser = await userManager.FindByIdAsync(user.Id);
            if (existingUser == null) return NotFound();

            // Update properties
            existingUser.Email = user.Email;
            existingUser.UserName = user.UserName;

            // Update roles
            foreach (var role in user.Roles)
            {
                if (role.IsSelected)
                {
                    if (!await userManager.IsInRoleAsync(existingUser, role.Name))
                    {
                        await userManager.AddToRoleAsync(existingUser, role.Name);
                    }
                }
                else
                {
                    if (await userManager.IsInRoleAsync(existingUser, role.Name))
                    {
                        await userManager.RemoveFromRoleAsync(existingUser, role.Name);
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}
