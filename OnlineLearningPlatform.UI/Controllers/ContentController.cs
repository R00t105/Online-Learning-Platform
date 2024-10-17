using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class ContentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Index

        [Authorize]
        public async Task<IActionResult> Index(int courseId, int? contentId)
        {
            var contents = await _unitOfWork.Contents.FindAllByExpress(c => c.CourseId == courseId);
            var contentsAlways = (List<Content>)contents;

            var contentTexts = contentId.HasValue
                ? await _unitOfWork.ContentTexts.FindAllByExpress(ct => ct.ContentId == contentId.Value)
                : new List<ContentText>();

            ViewBag.ContentTexts = contentTexts;
            ViewBag.CourseId = courseId;

            return View(contentsAlways);
        }

        #endregion

        #region Load Content

        [Authorize]
        public async Task<IActionResult> LoadContent(int contentId)
        {
            var contentTexts = await _unitOfWork.ContentTexts.FindAllByExpress(ct => ct.ContentId == contentId);
            return PartialView("_ContentDetailsPartial", contentTexts);
        } 

        #endregion


        public async Task<IActionResult> ShowTextOfContent(int contentId)
        {
            var content = await _unitOfWork.Contents.FindByExpress(c => c.Id == contentId);
            if (content == null)
            {
                return NotFound();
            }
            var contents = (await _unitOfWork.Contents.FindAllByExpress(c => c.CourseId == content.CourseId)).ToList();

            int currentIndex = contents.FindIndex(c => c.Id == contentId);

            var previousContent = (currentIndex > 0) ? contents[currentIndex - 1] : null;
            var nextContent = (currentIndex < contents.Count - 1) ? contents[currentIndex + 1] : null;

            ViewBag.Contents = contents;
            ViewBag.ContentTexts = await _unitOfWork.ContentTexts.FindAllByExpress(ct => ct.ContentId == contentId);
            ViewBag.PreviousContent = previousContent;
            ViewBag.NextContent = nextContent;

            return View();
        }

        // GET: Content/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = await _unitOfWork.Courses.GetAllAsync();
            return View();
        }

        // POST: Content/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,CourseId")] Content content)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Contents.AddAsync(content);
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the Courses list when the ModelState is invalid
            ViewBag.Courses = await _unitOfWork.Courses.GetAllAsync();
            return View(content);
        }


        // GET: Content/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _unitOfWork.Contents.GetByIdAsync(id.Value);
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Content/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CourseId")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.Contents.UpdateAsync(content);
                    await _unitOfWork.Complete();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ContentExists(content.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(content);
        }

        // GET: Content/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _unitOfWork.Contents.GetByIdAsync(id.Value);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // POST: Content/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = await _unitOfWork.Contents.GetByIdAsync(id);
            if (content != null)
            {
                await _unitOfWork.Contents.RemoveAsync(id);
                await _unitOfWork.Complete();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ContentExists(int id)
        {
            var content = await _unitOfWork.Contents.GetByIdAsync(id);
            return content != null;
        }

    }
}
