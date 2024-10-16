using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.BLL.Repositories;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
