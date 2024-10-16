using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _iunitOfWork;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserController(IUnitOfWork unitOfWork, RoleManager<IdentityRole<int>> roleManager, UserManager<ApplicationUser> userManager)
        {
            _iunitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        #region Create

        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = roles.Select(r => new { r.Id, r.Name }).ToList();
            return View("Create", new ApplicationUserViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.Name,
                    Email = userViewModel.Email,
                    BirthDate = userViewModel.BirthDate,
                    PasswordHash = userViewModel.Password
                };

                var result = await _userManager.CreateAsync(user, userViewModel.Password);

                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByIdAsync(userViewModel.RoleId.ToString());
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Role does not exist.");
                    }

                    return RedirectToAction("Users", "Dashboard");
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = roles.Select(r => new { r.Id, r.Name }).ToList();
            return View(userViewModel);
        }

        #endregion

        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            var user = _userManager.FindByIdAsync(id.ToString());
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = roles.Select(r => new { r.Id, r.Name }).ToList();
            return View("Edit", new ApplicationUserViewModel());
        } 

        #endregion


    }
}
