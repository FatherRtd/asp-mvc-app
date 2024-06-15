using System.Security.Claims;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcapp.Models.Users;

namespace mvcapp.Controllers
{
	public class UsersController : BaseController
	{
		private readonly AppDbContext _context;

		public UsersController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> LoginAsync([Bind(Prefix = "l")] LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("Index", new UsersViewModel
				{
					LoginViewModel = model
				});
			}

			var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

			if (user is null)
			{
				ViewBag.Error = "Неверный логин или пароль";
				return View("Index", new UsersViewModel
				{
					LoginViewModel = model
				});
			}

			await AuthenticateAsync(user);
			return RedirectToAction("Index", "Home");
		}

		private async Task AuthenticateAsync(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, $"{user.Name} {user.Surname}"),
            };

			var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		public async Task<IActionResult> RegisterAsync([Bind(Prefix = "r")] RegisterViewModel model)
        {
			if (!ModelState.IsValid)
            {
				return View("Index", new UsersViewModel
				{
					RegisterViewModel = model
				});
            }

			var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
			if (user != null)
            {
				ViewBag.Error = "Пользователь с таким логином уже существует";
				return View("Index", new UsersViewModel
				{
					RegisterViewModel = model
				});
            }

			user = new User
            {
				Email = model.Email,
				Surname = model.SurName,
				Name = model.Name,
				Password = model.Password,
            };

			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();

			await AuthenticateAsync(user);
			return RedirectToAction("Index", "Home");
        }

		public async Task<IActionResult> LogoutAsync()
        {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Users");
        }
	}
}
