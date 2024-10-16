using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRoleName = userRoles.FirstOrDefault();

            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == userRoleName);

            var UserModel = new ApplicationUserViewModel()
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                RoleId = role != null ? role.Id : 0

            };
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = roles.Select(r => new { r.Id, r.Name }).ToList();
            return View("Edit", UserModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationUserViewModel UserEdits)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = UserEdits.Name;
            user.Email = UserEdits.Email;
            user.BirthDate = UserEdits.BirthDate;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                var roles = await _roleManager.Roles.ToListAsync();
                ViewBag.Roles = roles.Select(r => new { r.Id, r.Name }).ToList();
                return View("Edit", UserEdits);
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var currentRoleName = currentRoles.FirstOrDefault();

            var newRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == UserEdits.RoleId);

            if (newRole != null && newRole.Name != currentRoleName)
            {
                if (currentRoleName != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, currentRoleName);
                }

                await _userManager.AddToRoleAsync(user, newRole.Name);
            }

            return RedirectToAction("Users", "Dashboard");
        }

        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var UserModel = new ApplicationUserViewModel()
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate,
                BirthDate = user.BirthDate
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            var userRoleName = userRoles.FirstOrDefault();

            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == userRoleName);

            UserModel.RoleId = role != null ? role.Id : 0;

            ViewBag.RoleName = role != null ? role.Name : "No Role";


            return View("Delete", UserModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Users", "Dashboard");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Delete", model: new ApplicationUserViewModel { Id = id });
        } 

        #endregion

    }
}
