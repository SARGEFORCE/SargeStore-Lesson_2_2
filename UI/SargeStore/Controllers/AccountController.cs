using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SargeStoreDomain.ViewModels.Identity;
using SargeStoreDomain.Entities.Identity;

namespace SargeStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignupManager, ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignupManager;
            _Logger = Logger;
        }

        public async Task<IActionResult> IsNameFree(string UserName)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user != null)
                return Json("Пользователь с таким именем уже существует");
            return Json("true");
        }

        public IActionResult Register() => View(new RegisterUserViewModel());
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid)
            {
                _Logger.LogWarning("Регистрация нового пользователя {0} прошла с ошибкой", Model.UserName);
                return View(Model);
            }

            var user = new User
            {
                UserName = Model.UserName
            };
            
            _Logger.LogInformation("Регистрация нового пользователя {0}", Model.UserName);

            var registration_result = await _UserManager.CreateAsync(user, Model.Password);

            if (registration_result.Succeeded)
            {
                await _UserManager.AddToRoleAsync(user, Role.User);
                _Logger.LogInformation("Пользователь {0} успешно зарегистрирован", Model.UserName);
                await _SignInManager.SignInAsync(user, false);
                _Logger.LogInformation("Пользователь {0} вошёл в систему", Model.UserName);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registration_result.Errors)
                ModelState.AddModelError("", error.Description);
            _Logger.LogWarning("Ошибка при регистрации нового пользователя {0}:{1}",
                Model.UserName, string.Join(", ", registration_result.Errors.Select(e => e.Description)));

            return View(Model);
        }
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl});
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                false);
            if (login_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} вошёл в систему", Model.UserName);
                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return Redirect(Model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");
            _Logger.LogWarning("Ошибка при входе пользователя {0} в систему", Model.UserName);
            return View(Model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            _Logger.LogInformation("Пользователь {0} вышел из системы",User.Identity.Name);
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenided() => View();
    }
}