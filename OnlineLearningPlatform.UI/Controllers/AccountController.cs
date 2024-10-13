using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IUnitOfWork IUnitOfWork, UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> SignInManager)
        {
            _iUnitOfWork = IUnitOfWork;
            _userManager = UserManager;
            _signInManager = SignInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserRegisterViewModel UserFromRequest)
        {
            //Mapping
            ApplicationUser User = new ApplicationUser();
            User.UserName = UserFromRequest.Name;
            User.Email = UserFromRequest.Email;
            User.PasswordHash = UserFromRequest.Password;

            // Save To Database
            var result = await _userManager.CreateAsync(User, User.PasswordHash);

            if (result.Succeeded)
            {
                // Create Cookie, false means it's Session
                await _signInManager.SignInAsync(User, false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var item in result.Errors)
                ModelState.AddModelError("", item.Description);

            return View(UserFromRequest);
        }
    }
}
