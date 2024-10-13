using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels;
using System.Security.Claims;

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
        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(UserRegisterViewModel UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                //Mapping
                ApplicationUser User = new ApplicationUser();
                User.UserName = UserFromRequest.UserName;
                User.Email = UserFromRequest.Email;
                User.PasswordHash = UserFromRequest.Password;

                // Save To Database
                var result = await _userManager.CreateAsync(User, User.PasswordHash);

                if (result.Succeeded)
                {
                    // Create Cookie, false means it's Session
                    await _signInManager.SignInAsync(User, false);
                    return RedirectToAction("Login");
                }
                foreach (var item in result.Errors)
                    ModelState.AddModelError("", item.Description);
            }

            return View(UserFromRequest);
        }
        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel UserLogin)
        {
            if (ModelState.IsValid)
            {
                // Check found
                var UserInDatabase = await _userManager.FindByNameAsync(UserLogin.UserName);
                if (UserInDatabase != null)
                {
                    var Found = await _userManager.CheckPasswordAsync(UserInDatabase, UserLogin.Password);
                    if (Found)
                    {
                        // Create Cookie
                        //var Claims = new List<Claim>() { new Claim("UserAddress", UserInDatabase.Address) };
                        await _signInManager.SignInAsync(UserInDatabase, UserLogin.RemmemberMe); //, Claims
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "UserName Or Password Wrong");
            }
            return View(UserLogin);
        }

        #endregion

        #region Logout

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion


    }
}
