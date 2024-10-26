using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Auth;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Auth;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Linkdev.Talabat.Dashboard.Controllers.Auth
{
    public class AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }        
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var appUser = await userManager.FindByEmailAsync(user.Email);

            if(appUser is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login!");
                return View(user);
            }

            var result = await signInManager.PasswordSignInAsync(appUser, user.Password, true, true);

            if(result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Email is has not been Confirmed yet!");
                return View(user);
            }

            if(result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Email is has benn Locked out!");
                return View(user);
            }

            if(!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login!");
                return View(user);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,

            };

            await userManager.CreateAsync(applicationUser, registerDto.Password);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
