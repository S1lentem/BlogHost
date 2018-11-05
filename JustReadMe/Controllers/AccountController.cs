using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using JustReadMe.Models;
using JustReadMe.ViewModels;
using JustReadMe.Protection;
using JustReadMe.Interfaces;
using JustReadMe.Interfaces.Repository;
using JustReadMe.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace JustReadMe.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHashable hashManager;
        private readonly IUserRepository users;
        private readonly IAuthenticationRegisterService usersManager;

        public AccountController(IUserRepository users, IHashable hashManager, IAuthenticationRegisterService usersManager)
        {
            this.users = users;
            this.hashManager = hashManager;
            this.usersManager = usersManager;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (usersManager.UserAuthentication(model, this.hashManager))
                {
                    await Authnticate(users.GetByEmail(model.Email).Nickname);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorect login and (or) passwords");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (usersManager.CreateNewUser(model, this.hashManager))
                {
                    await Authnticate(model.Nickname);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorect login and (or) passwords");
            }
            return View(model);
        }

        [Authorize]
        public IActionResult UserInfo() => View(users.GetByName(User.Identity.Name));

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task Authnticate(string userName)
        {
            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, userName) };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
