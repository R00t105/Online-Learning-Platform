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

        // POST: Track/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CreationDate,TrackId")] Course course)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Courses.AddAsync(course);
                _unitOfWork.Complete();
                return RedirectToAction("Courses", "Dashboard");
            }
            ViewBag.tracks= await _unitOfWork.Tracks.GetAllAsync();
            return View(course);
        }

        // GET: Track/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _unitOfWork.Courses.GetByIdAsync(id.Value);
            var tracks = await _unitOfWork.Tracks.GetAllAsync();
            ViewBag.tracks = tracks;
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Track/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreationDate,TrackId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.Courses.UpdateAsync(course);
                    _unitOfWork.Complete();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Courses", "Dashboard");
            }
            ViewBag.tracks = await _unitOfWork.Tracks.GetAllAsync();
            return View(course);
        }

        // GET: Track/Delete/5
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
            var track = await _unitOfWork.Tracks.GetByIdAsync(course.Id);
            ViewBag.trackname=track.Name;
            return View(course);
        }

        // POST: Track/Delete/5
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

        private async Task<bool> CourseExists(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            return course != null;
        }
    }
}
