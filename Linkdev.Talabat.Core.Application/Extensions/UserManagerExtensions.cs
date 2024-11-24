using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Abstraction.Models._Common;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Linkdev.Talabat.Core.Application.Extensions
{
	public static class UserManagerExtensions
	{
		public static async Task<ApplicationUser?> GetCurrentUserIncludingAddressAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal claimsPrincipal)
		{
			var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

			return await userManager.Users.Where(user => user.Email == email).Include(user => user.Address).FirstOrDefaultAsync();
		}
	}
}
