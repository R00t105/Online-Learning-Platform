using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        #region Index
       
        public async Task<IActionResult> Index()
        {
            var tracks = await _unitOfWork.Tracks.GetAllAsync();
            return View(tracks);
        }
        #endregion 


        #region ShowCourses

        [HttpGet]
        public async Task<IActionResult> ShowCourses(int trackId)
        {
            var Courses = await _unitOfWork.Courses.FindAllByExpress(c => c.TrackId == trackId);
            var Track = await _unitOfWork.Tracks.GetByIdAsync(trackId);

            ViewBag.TrackName = Track.Name.ToString();
            return View("ShowCourses", Courses);
        }
        #endregion


        #region Create
     
        public async Task<IActionResult> Create()
        {
            return View();
        }

       
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Track track)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Tracks.AddAsync(track);
                await _unitOfWork.Complete();
                return RedirectToAction("Tracks","Dashboard");
            }
            return View(track);
        }
        #endregion


        #region Edit
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
        #endregion
        

        #region Delete
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


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var track = await _unitOfWork.Tracks.GetByIdAsync(id);
            if (track != null)
            {
                await _unitOfWork.Tracks.RemoveAsync(id);
               await _unitOfWork.Complete();
            }
            return RedirectToAction("Tracks", "Dashboard");
        }
        #endregion


        #region TrackExists
        private async Task<bool> TrackExists(int id)
        {
            var track = await _unitOfWork.Tracks.GetByIdAsync(id);
            return track != null;
        }
        #endregion
    }
}
