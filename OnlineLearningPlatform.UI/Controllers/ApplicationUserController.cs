using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels;
using System.Security.Claims;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserController(IUnitOfWork IUnitOfWork, IEnrollmentRepository enrollmentRepository, UserManager<ApplicationUser> userManager)
        {
            _iUnitOfWork = IUnitOfWork;
            this.enrollmentRepository = enrollmentRepository;
            this.userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Profile(int id)
        {
            ApplicationUser user = await _iUnitOfWork.ApplicationUsers.GetByIdAsync(id);
            UserProfileViewModel model = new UserProfileViewModel();
            model.Id = id;
            model.UserName = user.UserName;
            model.BirhtDate = user.BirthDate;
            model.Email = user.Email;
            model.courses = await enrollmentRepository.GetCoursesByUserIdAsync(user.Id);
            return View(model);
        }
        public async Task<IActionResult> Edite(int id)
        {
            ApplicationUser user = await _iUnitOfWork.ApplicationUsers.GetByIdAsync(id);
            UserEditeViewModel model = new UserEditeViewModel();
            model.Email = user.Email;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edite(UserEditeViewModel New, int id)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _iUnitOfWork.ApplicationUsers.GetByIdAsync(id);
                user.Email = New.Email;
                var res = await userManager.ChangePasswordAsync(user, New.OldPassword, New.NewPassword);
                if (res.Succeeded)
                {
                    return Ok("Password changed successfully.");
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(New);  
         
        }

        public async Task<IActionResult> DeleteCourse(int userid,int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var enrollment = await _iUnitOfWork.Enrollments.FindByExpress(e => e.ApplicationUserId == int.Parse(userId) && e.CourseId == courseId);

            if (enrollment == null)
            {
                return NotFound("Enrollment not found.");
            }
             enrollmentRepository.Remove(userid,courseId);
            return Ok("Course Was Deleted");
        }
    }
}
