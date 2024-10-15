using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels; // Add this line to include the ViewModels
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.UI.Controllers
{

    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            ViewBag.Enrollments = await _unitOfWork.Enrollments.GetAllAsync();
            ViewBag.UsersNom = await _unitOfWork.ApplicationUsers.GetAllAsync();
            ViewBag.TracksNom = await _unitOfWork.Tracks.GetAllAsync();
            ViewBag.CoursesNom = await _unitOfWork.Courses.GetAllAsync();
            return View();
        } 
        #endregion

        #region Users
        public async Task<IActionResult> Users()
        {
            var users = await _unitOfWork.ApplicationUsers.GetAllAsync();
            var userViewModels = users.Select(user => new ApplicationUserViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate,
                BirthDate = user.BirthDate
            }).ToList();

            return PartialView(userViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([Bind("Id,Name,Email")] ApplicationUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.Name,
                    Email = userViewModel.Email,
                    RegistrationDate = userViewModel.RegistrationDate,
                    BirthDate = userViewModel.BirthDate
                };

                await _unitOfWork.ApplicationUsers.AddAsync(user);
                return RedirectToAction(nameof(Users));
            }
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(ApplicationUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.ApplicationUsers.GetByIdAsync(userViewModel.Id);
                if (user != null)
                {
                    user.UserName = userViewModel.Name;
                    user.Email = userViewModel.Email;
                    user.RegistrationDate = userViewModel.RegistrationDate;
                    user.BirthDate = userViewModel.BirthDate;

                    await _unitOfWork.ApplicationUsers.UpdateAsync(user);
                    return RedirectToAction(nameof(Users));
                }
            }
            return View(userViewModel);
        }

        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _unitOfWork.ApplicationUsers.GetByIdAsync(id);
            if (user != null)
            {
                await _unitOfWork.ApplicationUsers.RemoveAsync(id);
                return RedirectToAction(nameof(Users));
            }
            return NotFound();
        }
        #endregion

        #region Courses
        public async Task<IActionResult> Courses()
        {
            var courses = await _unitOfWork.Courses.GetAllAsync();
            var courseViewModels = courses.Select(course => new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreationDate = course.CreationDate,
                TrackId = course.TrackId
            }).ToList();

            return View(courseViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([Bind("Id,Title,Description,CreationDate,TrackId")] CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Title = courseViewModel.Title,
                    Description = courseViewModel.Description,
                    CreationDate = courseViewModel.CreationDate,
                    TrackId = courseViewModel.TrackId
                };

                await _unitOfWork.Courses.AddAsync(course);
                return RedirectToAction(nameof(Courses));
            }
            return View(courseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                var course = await _unitOfWork.Courses.GetByIdAsync(courseViewModel.Id);
                if (course != null)
                {
                    course.Title = courseViewModel.Title;
                    course.Description = courseViewModel.Description;
                    course.CreationDate = courseViewModel.CreationDate;
                    course.TrackId = courseViewModel.TrackId;

                    await _unitOfWork.Courses.UpdateAsync(course);
                    return RedirectToAction(nameof(Courses));
                }
            }
            return View(courseViewModel);
        }

        [HttpPost, ActionName("DeleteCourse")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course != null)
            {
                await _unitOfWork.Courses.RemoveAsync(id);
                return RedirectToAction(nameof(Courses));
            }
            return NotFound();
        }
        #endregion

        #region Tracks
        public async Task<IActionResult> Tracks()
        {
            var Tracks = await _unitOfWork.Tracks.GetAllAsync();
            var trackViewModels = Tracks.Select(track => new TrackViewModel
            {
                Id = track.Id,
                Name = track.Name,
                Description = track.Description,
            }).ToList();

            return View(trackViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrack(TrackViewModel trackViewModel)
        {
            if (ModelState.IsValid)
            {
                var track = new Track
                {
                    Name = trackViewModel.Name,
                    Description = trackViewModel.Description
                };

                await _unitOfWork.Tracks.AddAsync(track);
                return RedirectToAction(nameof(Courses));
            }
            return View(trackViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditTrack(int id, TrackViewModel trackViewModel)
        {
            if (ModelState.IsValid)
            {
                var track = await _unitOfWork.Tracks.GetByIdAsync(id);
                if (track != null)
                {
                    track.Name = trackViewModel.Name;
                    track.Description = trackViewModel.Description;

                    await _unitOfWork.Tracks.UpdateAsync(track);
                    return RedirectToAction(nameof(Tracks));
                }
            }
            return View(trackViewModel);
        }

        [HttpPost, ActionName("DeleteTrack")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            var track = await _unitOfWork.Tracks.GetByIdAsync(id);
            if (track != null)
            {
                await _unitOfWork.Tracks.RemoveAsync(id);
                return RedirectToAction(nameof(Tracks));
            }
            return NotFound();
        }
        #endregion

        #region Content
        public async Task<IActionResult> Content()
        {
            var contentItems = await _unitOfWork.Contents.GetAllAsync();
            var contentViewModels = contentItems.Select(content => new ContentViewModel
            {
                Id = content.Id,
                Title = content.Title,
                VideoUrl = content.VideoUrl,
                CourseId = content.CourseId
            }).ToList();

            return View(contentViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContent([Bind("Id,Title,VideoUrl,CourseId")] ContentViewModel contentViewModel)
        {
            if (ModelState.IsValid)
            {
                var content = new Content
                {
                    Title = contentViewModel.Title,
                    VideoUrl = contentViewModel.VideoUrl,
                    CourseId = contentViewModel.CourseId
                };

                await _unitOfWork.Contents.AddAsync(content);
                return RedirectToAction(nameof(Content));
            }
            return View(contentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditContent(ContentViewModel contentViewModel)
        {
            if (ModelState.IsValid)
            {
                var content = await _unitOfWork.Contents.GetByIdAsync(contentViewModel.Id);
                if (content != null)
                {
                    content.Title = contentViewModel.Title;
                    content.VideoUrl = contentViewModel.VideoUrl;
                    content.CourseId = contentViewModel.CourseId;

                    await _unitOfWork.Contents.UpdateAsync(content);
                    return RedirectToAction(nameof(Content));
                }
            }
            return View(contentViewModel);
        }

        [HttpPost, ActionName("DeleteContent")]
        public async Task<IActionResult> DeleteContent(int id)
        {
            var contentItem = await _unitOfWork.Contents.GetByIdAsync(id);
            if (contentItem != null)
            {
                await _unitOfWork.Contents.RemoveAsync(id);
                return RedirectToAction(nameof(Content));
            }
            return NotFound();
        }
        #endregion

        #region Enrollment
        public async Task<IActionResult> Enrollments()
        {
            var enrollments = await _unitOfWork.Enrollments.GetAllAsync();
            var enrollmentViewModels = enrollments.Select(enrollment => new EnrollmentViewModel
            {
                Id = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                ProgressState = enrollment.ProgressState,
                CompletionDate = enrollment.CompletionDate,
                ProgressPercentage = enrollment.ProgressPercentage,
                ApplicationUserId = enrollment.ApplicationUserId,
                CourseId = enrollment.CourseId
            }).ToList();

            return View(enrollmentViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment([Bind("Id,EnrollmentDate,ProgressState,CompletionDate,ProgressPercentage,ApplicationUserId,CourseId")] EnrollmentViewModel enrollmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var enrollment = new Enrollment
                {
                    EnrollmentDate = enrollmentViewModel.EnrollmentDate,
                    ProgressState = enrollmentViewModel.ProgressState,
                    CompletionDate = enrollmentViewModel.CompletionDate,
                    ProgressPercentage = enrollmentViewModel.ProgressPercentage,
                    ApplicationUserId = enrollmentViewModel.ApplicationUserId,
                    CourseId = enrollmentViewModel.CourseId
                };

                await _unitOfWork.Enrollments.AddAsync(enrollment);
                return RedirectToAction(nameof(Enrollments));
            }
            return View(enrollmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditEnrollment(EnrollmentViewModel enrollmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(enrollmentViewModel.Id);
                if (enrollment != null)
                {
                    enrollment.EnrollmentDate = enrollmentViewModel.EnrollmentDate;
                    enrollment.ProgressState = enrollmentViewModel.ProgressState;
                    enrollment.CompletionDate = enrollmentViewModel.CompletionDate;
                    enrollment.ProgressPercentage = enrollmentViewModel.ProgressPercentage;
                    enrollment.ApplicationUserId = enrollmentViewModel.ApplicationUserId;
                    enrollment.CourseId = enrollmentViewModel.CourseId;

                    await _unitOfWork.Enrollments.UpdateAsync(enrollment);
                    return RedirectToAction(nameof(Enrollments));
                }
            }
            return View(enrollmentViewModel);
        }

        [HttpPost, ActionName("DeleteEnrollment")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (enrollment != null)
            {
                await _unitOfWork.Enrollments.RemoveAsync(id);
                return RedirectToAction(nameof(Enrollments));
            }
            return NotFound();
        }
        #endregion
    }
}
