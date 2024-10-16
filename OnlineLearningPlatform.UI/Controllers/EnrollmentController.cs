using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.BLL.Repositories;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View("Enrollments", "Dashboard");
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.users = await _unitOfWork.ApplicationUsers.GetAllAsync();
            ViewBag.courses = await _unitOfWork.Courses.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationUserId,CourseId,EnrollmentDate,ProgressState,CompletionDate,ProgressPercentage")] EnrollmentViewModel enrollmentViewModel)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Enrollments.AddAsync(new Enrollment
                {
                    ApplicationUserId = enrollmentViewModel.ApplicationUserId
                    ,
                    CourseId = enrollmentViewModel.CourseId,
                    EnrollmentDate = enrollmentViewModel.EnrollmentDate,
                    ProgressState = enrollmentViewModel.ProgressState,
                    CompletionDate= enrollmentViewModel.CompletionDate,
                    ProgressPercentage= enrollmentViewModel.ProgressPercentage
                });
                return RedirectToAction("Enrollments", "Dashboard");
            }
            ViewBag.users = await _unitOfWork.ApplicationUsers.GetAllAsync();
            ViewBag.courses = await _unitOfWork.Courses.GetAllAsync();
            return View(enrollmentViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? ApplicationUserId, int? CourseId)
        {
            if (ApplicationUserId == null || CourseId == null)
            {
                return NotFound();
            }

            var enroll = await _unitOfWork.Enrollments.GetEnrollby2keysasync(ApplicationUserId.Value, CourseId.Value);
            if (enroll == null)
            {
                return NotFound();
            }
            EnrollmentViewModel enrollmentViewModel = new EnrollmentViewModel();
            enrollmentViewModel.ApplicationUserId = enroll.ApplicationUserId;
            enrollmentViewModel.CourseId= enroll.CourseId;
            enrollmentViewModel.EnrollmentDate= enroll.EnrollmentDate;
            enrollmentViewModel.ProgressState= enroll.ProgressState;
            enrollmentViewModel.CompletionDate= enroll.CompletionDate;
            enrollmentViewModel.ProgressPercentage= enroll.ProgressPercentage;

            var username = await _unitOfWork.ApplicationUsers.GetByIdAsync(enrollmentViewModel.ApplicationUserId);
            var coursetitle = await _unitOfWork.Courses.GetByIdAsync(enrollmentViewModel.CourseId);
            ViewBag.Username = username.UserName.ToString();
            ViewBag.Coursetitle = coursetitle.Title.ToString();
            return View(enrollmentViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ApplicationUserId,int CourseId, [Bind("ApplicationUserId,CourseId,EnrollmentDate,ProgressState,CompletionDate,ProgressPercentage")] EnrollmentViewModel enrollmentViewModel)
        {
            if (ApplicationUserId != enrollmentViewModel.ApplicationUserId || CourseId != enrollmentViewModel.CourseId)
            {
                return NotFound();
            }
            var enrollment = await _unitOfWork.Enrollments.GetFirstOrDefaultWithIncludeAsync(e => e.ApplicationUserId == ApplicationUserId && e.CourseId == CourseId, e => e.ApplicationUser);

        

            if (enrollment == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
     
                enrollment.ApplicationUserId = enrollmentViewModel.ApplicationUserId;
                enrollment.CourseId= enrollmentViewModel.CourseId;
                enrollment.EnrollmentDate = enrollmentViewModel.EnrollmentDate;
                enrollment.ProgressState= enrollmentViewModel.ProgressState;
                enrollment.CompletionDate= enrollmentViewModel.CompletionDate;
                enrollment.ProgressPercentage= enrollmentViewModel.ProgressPercentage;
                await _unitOfWork.Enrollments.UpdateAsync(enrollment);
                return RedirectToAction("Enrollments", "Dashboard");
            }
            var username = await _unitOfWork.ApplicationUsers.GetByIdAsync(enrollmentViewModel.ApplicationUserId);
            var coursetitle = await _unitOfWork.Courses.GetByIdAsync(enrollmentViewModel.CourseId);
            ViewBag.Username = username.UserName.ToString();
            ViewBag.Coursetitle = coursetitle.Title.ToString();

            return View(enrollmentViewModel);
        }


        public async Task<IActionResult> Delete(int? ApplicationUserId, int? CourseId)
        {
            if (ApplicationUserId == null || CourseId == null)
            {
                return NotFound();
            }

            var enroll = await _unitOfWork.Enrollments.GetEnrollby2keysasync(ApplicationUserId.Value, CourseId.Value);

            if (enroll == null)
            {
                return NotFound();
            }
            EnrollmentViewModel enrollmentViewModel = new EnrollmentViewModel();
            enrollmentViewModel.ApplicationUserId = enroll.ApplicationUserId;
            enrollmentViewModel.CourseId = enroll.CourseId;
            enrollmentViewModel.EnrollmentDate = enroll.EnrollmentDate;
            enrollmentViewModel.ProgressState = enroll.ProgressState;
            enrollmentViewModel.CompletionDate = enroll.CompletionDate;
            enrollmentViewModel.ProgressPercentage = enroll.ProgressPercentage;
             var username = await _unitOfWork.ApplicationUsers.GetByIdAsync(enrollmentViewModel.ApplicationUserId);
             var coursetitle = await _unitOfWork.Courses.GetByIdAsync(enrollmentViewModel.CourseId);
            ViewBag.Username = username.UserName.ToString();
            ViewBag.Coursetitle = coursetitle.Title.ToString();
            return View(enrollmentViewModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? ApplicationUserId, int? CourseId)
        {
            var enroll = await _unitOfWork.Enrollments.GetEnrollby2keysasync(ApplicationUserId.Value, CourseId.Value);
            if (enroll == null)
            {
                return NotFound();
            }
            _unitOfWork.Enrollments.Remove(ApplicationUserId.Value, CourseId.Value);
            return RedirectToAction("Enrollments", "Dashboard");
        }

        #region Enroll


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Enroll(int courseId)
        {

            if (courseId == null)
            {
                return NotFound();
            }

            var course = await _unitOfWork.Courses.GetByIdAsync(courseId);

            if (course == null)
            {
                return NotFound();
            }
            
            return View(course);
        }



        [HttpPost, ActionName("Enroll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollConfirmed(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);
            var course = await _unitOfWork.Courses.GetByIdAsync(courseId);


            if (course == null)
            {
                return NotFound("Course not found.");
            }


            var existingEnrollment = await _unitOfWork.Enrollments.FindByExpress(e => e.CourseId == courseId && e.ApplicationUserId == user.Id);
            if (existingEnrollment != null)
            {
                ModelState.AddModelError("", "You are already enrolled in this course.");
                return View("Enroll", course);
            }

            var enrollment = new Enrollment
            {
                ApplicationUserId = user.Id,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now
            };

            await _unitOfWork.Enrollments.AddAsync(enrollment);
            await _unitOfWork.Complete();

            ViewBag.SuccessMessage = "You have successfully enrolled in the course!";
            return View("Enroll", course);
        } 

        #endregion

    }
}
