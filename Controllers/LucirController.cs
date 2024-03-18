using Microsoft.AspNetCore.Mvc;
using LucirWeb_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System;

namespace LucirWeb_MVC.Controllers
{
    
    public class LucirController : Controller
	{
        public IActionResult Index()
		{
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Users");
            }
			return View();
		}
        private Student o = new();
        [Authorize]
        public IActionResult Users()
        {
            List<Student> l = o.GetAllStudents();
            return View(l);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Lucir model)
        {
            try
            {
                model.Login();
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Un),
                    new Claim("OtherProperties","Example Role")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                  CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = model.KLI
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Users");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("ErrKey", ex.Message);
                return View(model);

            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

    }
}
