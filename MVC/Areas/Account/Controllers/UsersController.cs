#nullable disable
using Business.Models;
using Business.Models.Account;
using Business.Services;
using Core.Results.Bases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

//Generated from Custom Template.
namespace MVC.Areas.Account.Controllers
{
    [Area("Account")]
    public class UsersController : Controller
    {
        // Add service injections here
        private readonly IAccountService _accountService;

		public UsersController(IAccountService accountService)
		{
			_accountService = accountService;
		}


        // GET: Account/Users/Login
        public IActionResult Login(string returnUrl)
        {
            AccountLoginModel model = new AccountLoginModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        // POST: Account/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AccountLoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel userResultModel = new UserModel();

                Result result = _accountService.Login(model, userResultModel);

                if (result.IsSuccessful)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, userResultModel.UserName),
                        new Claim(ClaimTypes.Role, userResultModel.Role.Name),
                        new Claim(ClaimTypes.Sid, userResultModel.Id.ToString())
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                ModelState.AddModelError("", result.Message);
            }

            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult AccessDenied()
        {
            return View("_Error", "Access is denied for this page!");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Result result = _accountService.Register(model);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Login));
                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }
    }
}
