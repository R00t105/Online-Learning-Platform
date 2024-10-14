using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

     
        public async Task<IActionResult> Index()
        {
            var Courses = await _unitOfWork.Courses.GetAllAsync();
            return View(Courses);
        }
        public async Task<IActionResult> CourseData(int? id)
        {
            CourseWithContentsViewModel courseWithContents = new CourseWithContentsViewModel();
            if (id == null)
            {
                return NotFound();
            }

            var course = await _unitOfWork.Courses.GetFirstOrDefaultWithIncludeAsync(c=>c.Id==id,c=>c.Contents);
            var track = await _unitOfWork.Tracks.FindByExpress(t=>t.Id== course.TrackId);
            
            if (course == null)
            {
                return NotFound();
            }
            courseWithContents.Id = course.Id;
            courseWithContents.Name = course.Title;
            courseWithContents.Description = course.Description;
            courseWithContents.TrackName = track.Name;
            courseWithContents.contents = course.Contents.ToList();
            courseWithContents.CreatedDate = course.CreationDate;
            courseWithContents.TrackId = track.Id;

            return View(courseWithContents);
        }
    }
}
