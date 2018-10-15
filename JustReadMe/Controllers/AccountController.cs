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

namespace JustReadMe.Controllers
{
    public class AccountController : Controller
    {
        private BloghostContext db;
        private IHashable passwordManager;

        public AccountController(BloghostContext context) => this.db = context;

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
                passwordManager = new PasswordHashingManager(model.Passwords);
                UserModel user = await db.Users.FirstOrDefaultAsync(userModel => userModel.Email == model.Email);
                if (user != null && passwordManager.VerifyPassword(user.Password))
                {
                    await Authnticate(user.Email);
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
                UserModel user = await db.Users.FirstOrDefaultAsync(userModel => userModel.Email == model.Email);
                if (user == null)
                {
                    passwordManager = new PasswordHashingManager(model.Password);
                    db.Users.Add(new UserModel()
                    {
                        Email = model.Email,
                        Password = passwordManager.GetHash(),
                        Nickname = model.Nickname
                    });
                    await db.SaveChangesAsync();
                    await Authnticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorect login and (or) passwords");
            }
            return View(model);
        }

        public async Task<IActionResult> UserInfo()
        {
            UserModel currentUser = await db.Users.FirstOrDefaultAsync(userModel => userModel.Email == User.Identity.Name);
            return View(currentUser);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authnticate(string userName)
        {
            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, userName) };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
