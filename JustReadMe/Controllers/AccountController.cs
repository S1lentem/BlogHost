using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using BlogHostCore.Interfaces.Repository;
using BlogHostCore.Interfaces;
using BlogHostCore.Interfaces.Services;
using Web.ViewModels;
using BlogHostCore.DomainModels;
using System;

namespace Web.Controllers
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
        [Route("authorization")]
        public IActionResult Login() => View();

        [HttpGet]
        [Route("registration")]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("authorization")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (usersManager.UserAuthentication(model.Email, model.Passwords, this.hashManager))
                {
                    await Authnticate(users.GetFullInfoByEmail(model.Email));
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
                if (usersManager.CreateNewUser(model.Nickname, model.Email, model.Password, this.hashManager))
                {
                    await Authnticate(users.GetFullInfoByName(model.Nickname));
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorect login and (or) passwords");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        [Route("user_info")]
        public IActionResult UserInfo() => View(users.GetFullInfoByName(User.Identity.Name));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult SetRoleForUser(string userName, string role)
        {
            try
            {
                users.SetRole(userName, role);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction("RolliesControl");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("role_manager")]
        public IActionResult RolliesControl() => View();


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task Authnticate(UserInfo user)
        {
            Console.WriteLine(user.Role.ToString());
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Nickname),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
