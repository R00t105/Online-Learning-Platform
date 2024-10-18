using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
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
        private readonly UserManager<ApplicationUser> _userManager;
        static string SearchType;

        public DashboardController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            SearchType = "Index";
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
                BirthDate = user.BirthDate,
                Roles = _userManager.GetRolesAsync(user).Result.ToList()

            }).ToList();

            return PartialView(userViewModels);
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
        public async Task<IActionResult> Contents()
        {
            SearchType = "Contents";
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

        #endregion

        #region ContentTexts
        public async Task<IActionResult> ContentTexts()
        {
            SearchType = "ContentTexts";
            var contenttextsItems = await _unitOfWork.ContentTexts.GetAllAsync();
            var contenttextViewModels = contenttextsItems.Select(content => new ContentTextViewModel
            {
                Id = content.Id,
                Title = content.Title,
                SubTitle = content.SubTitle,
                ContentId = content.ContentId
            }).ToList();

            return View(contenttextViewModels);
        }
        #endregion

        #region Enrollment
        public async Task<IActionResult> Enrollments()
        {
            SearchType = "Enrollments";
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

            ViewBag.Enrollments = await _unitOfWork.Enrollments.GetAllAsync();

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
                    BirthDate = u.BirthDate,
                    Roles = _userManager.GetRolesAsync(u).Result.ToList()
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
            else if (SearchType == "Contents")
            {
                if (string.IsNullOrEmpty(searchquery))
                {
                    return RedirectToAction("Contents");
                }
                var contentItems = await _unitOfWork.Contents.FindAllByExpress(c => c.Title.Contains(searchquery));
                var contentViewModels = contentItems.Select(content => new ContentViewModel
                {
                    Id = content.Id,
                    Title = content.Title,
                    VideoUrl = content.VideoUrl,
                    CourseId = content.CourseId
                }).ToList();
                return View("Contents", contentViewModels);
            }
            else if(SearchType== "ContentTexts")
            {
                if (string.IsNullOrEmpty(searchquery))
                {
                    return RedirectToAction("ContentTexts");
                }
                var contenttextItems = await _unitOfWork.ContentTexts.FindAllByExpress(c => c.Title.Contains(searchquery));
                var contenttextViewModels = contenttextItems.Select(content => new ContentTextViewModel
                {
                    Id = content.Id,
                    Title = content.Title,
                    SubTitle = content.SubTitle,
                    ContentId = content.ContentId
                }).ToList();
                return View("ContentTexts", contenttextViewModels);
            }
            else if(SearchType== "Tracks")
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
            else if(SearchType== "Enrollments")
            {
              
              return RedirectToAction("Enrollments");
              
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
          #endregion
    }
}