using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class TrackController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrackController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Track/Index
        public async Task<IActionResult> Index()
        {
            var tracks = await _unitOfWork.Tracks.GetAllAsync();
            return View(tracks);
        }


        [HttpGet]
        public async Task<IActionResult> ShowCoursesByTrack(int trackId)
        {
            var Courses = await _unitOfWork.Courses.FindAllByExpress(c => c.TrackId == trackId);
            var Track = await _unitOfWork.Tracks.GetByIdAsync(trackId);
            ViewBag.TrackName = Track.Name.ToString();
            return View(Courses);
        }



        // GET: Track/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _unitOfWork.Tracks.GetByIdAsync(id.Value);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: Track/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Track/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreationDate")] Track track)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Tracks.AddAsync(track);
                _unitOfWork.Complete();
                return RedirectToAction("Tracks","Dashboard");
            }
            return View(track);
        }

        // GET: Track/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _unitOfWork.Tracks.GetByIdAsync(id.Value);
            if (track == null)
            {
                return NotFound();
            }
            return View(track);
        }

        // POST: Track/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreationDate")] Track track)
        {
            if (id != track.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.Tracks.UpdateAsync(track);
                    _unitOfWork.Complete();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TrackExists(track.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Tracks", "Dashboard");
            }
            return View(track);
        }

        // GET: Track/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _unitOfWork.Tracks.GetByIdAsync(id.Value);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: Track/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var track = await _unitOfWork.Tracks.GetByIdAsync(id);
            if (track != null)
            {
                _unitOfWork.Tracks.RemoveAsync(id);
                _unitOfWork.Complete();
            }
            return RedirectToAction("Tracks", "Dashboard");
        }

        private async Task<bool> TrackExists(int id)
        {
            var track = await _unitOfWork.Tracks.GetByIdAsync(id);
            return track != null;
        }
    }
}
