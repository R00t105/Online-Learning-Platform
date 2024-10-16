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
        static string SearchType;

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
            SearchType = "Users";
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
        public async Task<IActionResult> CreateUser(ApplicationUserViewModel userViewModel)
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
            SearchType = "Courses";
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

        
        #endregion

        #region Tracks
        public async Task<IActionResult> Tracks()
        {
            SearchType = "Tracks";
            var Tracks = await _unitOfWork.Tracks.GetAllAsync();
            var trackViewModels = Tracks.Select(track => new TrackViewModel
            {
                Id = track.Id,
                Name = track.Name,
                Description = track.Description,
            }).ToList();

            return View(trackViewModels);
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

        //[HttpPost]
        //public async Task<IActionResult> CreateContent([Bind("Id,Title,VideoUrl,CourseId")] ContentViewModel contentViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var content = new Content
        //        {
        //            Title = contentViewModel.Title,
        //            VideoUrl = contentViewModel.VideoUrl,
        //            CourseId = contentViewModel.CourseId
        //        };

        //        await _unitOfWork.Contents.AddAsync(content);
        //        return RedirectToAction(nameof(Content));
        //    }
        //    return View(contentViewModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditContent(ContentViewModel contentViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var content = await _unitOfWork.Contents.GetByIdAsync(contentViewModel.Id);
        //        if (content != null)
        //        {
        //            content.Title = contentViewModel.Title;
        //            content.VideoUrl = contentViewModel.VideoUrl;
        //            content.CourseId = contentViewModel.CourseId;

        //            await _unitOfWork.Contents.UpdateAsync(content);
        //            return RedirectToAction(nameof(Content));
        //        }
        //    }
        //    return View(contentViewModel);
        //}

        //[HttpPost, ActionName("DeleteContent")]
        //public async Task<IActionResult> DeleteContent(int id)
        //{
        //    var contentItem = await _unitOfWork.Contents.GetByIdAsync(id);
        //    if (contentItem != null)
        //    {
        //        await _unitOfWork.Contents.RemoveAsync(id);
        //        return RedirectToAction(nameof(Content));
        //    }
        //    return NotFound();
        //}
        #endregion

        #region Enrollment
        public async Task<IActionResult> Enrollments()
        { 
            var enrollments = await _unitOfWork.Enrollments.GetAllAsync();
            var enrollmentViewModels = enrollments.Select(enrollment => new EnrollmentViewModel
            {
                //Id = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                ProgressState = enrollment.ProgressState,
                CompletionDate = enrollment.CompletionDate,
                ProgressPercentage = enrollment.ProgressPercentage,
                ApplicationUserId = enrollment.ApplicationUserId,
                CourseId = enrollment.CourseId
            }).ToList();

            return View(enrollmentViewModels);
        }


        #endregion

        #region Search
        public async Task<IActionResult> Search(string searchquery)
        {

            if (SearchType == "Users")
            {
                    if (string.IsNullOrEmpty(searchquery))
                    {
                       return RedirectToAction("Users");
                     }
                var users = await _unitOfWork.ApplicationUsers.FindAllByExpress(u => u.UserName.Contains(searchquery));
                var userviewmodel = users.Select(u => new ApplicationUserViewModel
                {
                    Id = u.Id,
                    Name = u.UserName,
                    Email = u.Email,
                    RegistrationDate = u.RegistrationDate,
                    BirthDate = u.BirthDate
                }).ToList();

                return View("Users", userviewmodel);
            }
            else if (SearchType == "Courses")
            {
                if (string.IsNullOrEmpty(searchquery))
                {
                    return RedirectToAction("Courses");
                }
                var courses = await _unitOfWork.Courses.FindAllByExpress(c=>c.Title.Contains(searchquery));
                var courseViewModels = courses.Select(course => new CourseViewModel
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    CreationDate = course.CreationDate,
                    TrackId = course.TrackId
                }).ToList();
                return View("Courses", courseViewModels);
            }
            else
            {
                if (string.IsNullOrEmpty(searchquery))
                {
                    return RedirectToAction("Tracks");
                }
                var Tracks = await _unitOfWork.Tracks.FindAllByExpress(t=>t.Name.Contains(searchquery));
                var trackViewModels = Tracks.Select(track => new TrackViewModel
                {
                    Id = track.Id,
                    Name = track.Name,
                    Description = track.Description,
                }).ToList();
                return View("Tracks", trackViewModels);
            }
        }
          #endregion
    }
}