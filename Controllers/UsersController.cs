using AgileRap_Process.Data;
using AgileRap_Process.Models;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace AgileRap_Process.Controllers
{
	public class UsersController : Controller
	{
		private AgileRap_ProcessContext db = new AgileRap_ProcessContext();
		public IActionResult Login()
		{
			ClaimsPrincipal claimUser = HttpContext.User;

			if (claimUser.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Work");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(User user)
		{
			var inUser = db.User.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
			if (inUser != null)
			{
				HttpContext.Session.SetString("UserSession", user.Email);
				List<Claim> claims = new List<Claim>()
				{
					new Claim(ClaimTypes.NameIdentifier, user.Email),
					new Claim("OtherProperties","Example Role")
				};

				ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				AuthenticationProperties properties = new AuthenticationProperties()
				{
					AllowRefresh = true,
					IsPersistent = user.KeepLogin,
				};

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity), properties);

				return RedirectToAction("Index", "Works");
			}

			ViewData["ValidateMessage"] = "Email Or Password not correct";
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(User user)
		{
			var invalideEmail = db.User.Where(x => x.Email.Contains(user.Email)).FirstOrDefault();
			if (user.Email == null || user.Password == null)
			{
                TempData["EmailX"] = "Please enter your email address.";
                TempData["PasswordX"] = "Please enter your password.";
                return RedirectToAction("Register");
            }
			if (user.Password != user.ConfirmPassword)
			{
				TempData["PasswordNotmatch"] = "Password not match";
				return RedirectToAction("Register");
			}
			if (invalideEmail == null)
			{
				user.IsDelete = false;
				db.User.Add(user);
				db.SaveChanges();
				return RedirectToAction("Login");
			}
			ViewData["ValidateMessage"] = "Email is already taken";
			return View();
		}
	}
}
