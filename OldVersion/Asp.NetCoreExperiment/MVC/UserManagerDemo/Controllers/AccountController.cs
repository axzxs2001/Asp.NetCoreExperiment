using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace UserManagerDemo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]

        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]

        public async Task<IActionResult> Login(string username, string password)
        {
            await _signInManager.PasswordSignInAsync(username, password, false, false);

            return View();
        }
    }
}