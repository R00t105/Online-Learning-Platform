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
        public async Task<IActionResult> Create()
        {
            var tracks = await _unitOfWork.Tracks.GetAllAsync();
            ViewBag.tracks = tracks;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CreationDate,TrackId")] CourseViewModel courseViewModel)
        {
            
            if (ModelState.IsValid)
            {
                await _unitOfWork.Courses.AddAsync(new Course { Title=courseViewModel.Title ,Description =courseViewModel.Description , CreationDate = courseViewModel.CreationDate , TrackId = courseViewModel.TrackId });
                return RedirectToAction("Courses", "Dashboard");
            }
            ViewBag.tracks= await _unitOfWork.Tracks.GetAllAsync();
            return View(courseViewModel);
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CourseViewModel course =new CourseViewModel();
            var coursedb = await _unitOfWork.Courses.GetByIdAsync(id.Value);
            course.Id = coursedb.Id;
            course.Title = coursedb.Title;
            course.Description = coursedb.Description;
            course.CreationDate = coursedb.CreationDate;
            course.TrackId = coursedb.TrackId;
            var tracks = await _unitOfWork.Tracks.GetAllAsync();
            ViewBag.tracks = tracks;
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CreationDate,TrackId")] CourseViewModel course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                   Course realcourse = new Course();
                   realcourse.Id = course.Id;
                    realcourse.Title = course.Title;
                     realcourse.Description = course.Description;
                    realcourse.CreationDate = course.CreationDate;
                    realcourse.TrackId = course.TrackId;
                    await _unitOfWork.Courses.UpdateAsync(realcourse);
                          _unitOfWork.Complete();
                return RedirectToAction("Courses", "Dashboard");
            }
            ViewBag.tracks = await _unitOfWork.Tracks.GetAllAsync();
            return View(course);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _unitOfWork.Courses.GetByIdAsync(id.Value);
            
            if (course == null)
            {
                return NotFound();
            }
            var track = await _unitOfWork.Tracks.GetByIdAsync(course.TrackId);
            ViewBag.trackname=track.Name.ToString();
            return View(course);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course != null)
            {
                _unitOfWork.Courses.RemoveAsync(id);
                _unitOfWork.Complete();
            }
            return RedirectToAction("Courses", "Dashboard");
        }


    }
}
