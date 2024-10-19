using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels;
using System.Security.Claims;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class ContentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContentController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
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

        #region LoadContent
        [Authorize]
        public async Task<IActionResult> LoadContent(int contentId)
        {
            var contentTexts = await _unitOfWork.ContentTexts.FindAllByExpress(ct => ct.ContentId == contentId);
            return PartialView("_ContentDetailsPartial", contentTexts);
        }
        #endregion

        #region ShowTextOfContent




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
        #endregion

        #region Create

        // GET: Content/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = await _unitOfWork.Courses.GetAllAsync();
            return View();
        }

        // POST: Content/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,VideoUrl,CourseId")] ContentViewModel contentViewModel)
        {
            if (ModelState.IsValid)
            {
                Content content = new Content();
                content.Id = contentViewModel.Id;
                content.Title = contentViewModel.Title;
                content.VideoUrl = contentViewModel.VideoUrl;
                content.CourseId = contentViewModel.CourseId;
                await _unitOfWork.Contents.AddAsync(content);
                return RedirectToAction("Contents","Dashboard");
            }

            // Repopulate the Courses list when the ModelState is invalid
            ViewBag.Courses = await _unitOfWork.Courses.GetAllAsync();
            return View(contentViewModel);
        }
        #endregion

        #region Edit
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
            ContentViewModel contentViewModel = new ContentViewModel() { Id=content.Id,Title=content.Title,VideoUrl=content.VideoUrl,CourseId=content.CourseId};
            ViewBag.Courses = await _unitOfWork.Courses.GetAllAsync();
            return View(contentViewModel);
        }
        


        // POST: Content/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,VideoUrl,CourseId")] ContentViewModel contentViewModel)
        {
            if (id != contentViewModel.Id)
            {
                return NotFound();
            }
            Content content = await _unitOfWork.Contents.GetByIdAsync(id);
            content.Title= contentViewModel.Title;
            content.VideoUrl= contentViewModel.VideoUrl;
            content.CourseId= contentViewModel.CourseId;

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
                return RedirectToAction("Contents", "Dashboard");
            }
            ViewBag.Courses = await _unitOfWork.Courses.GetAllAsync();
            return View(contentViewModel);
        }
        #endregion

        #region Delete
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
            Course course=await _unitOfWork.Courses.GetByIdAsync(content.CourseId);
            ViewBag.coursename=course.Title.ToString();

            return View(content);
        }

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
            return RedirectToAction("Contents", "Dashboard");
        }
        #endregion

        #region ContentExists
        private async Task<bool> ContentExists(int id)
        {
            var content = await _unitOfWork.Contents.GetByIdAsync(id);
            return content != null;
        }
        #endregion

        #region Progress Handling

        [HttpPost]
        public async Task<IActionResult> MarkAsReadAsync(int contentId, bool isRead)
        {
            var content = await _unitOfWork.Contents.GetByIdAsync(contentId);
            if (content == null)
            {
                return NotFound();
            }

            content.IsRead = isRead;
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> CalculateProgressAsync(int courseId)
        {
            var contents = (List<Content>) await _unitOfWork.Contents.FindAllByExpress(c => c.CourseId == courseId);

            if (contents == null || contents.Count == 0)
            {
                return Json(0);
            }

            var readContentsCount = contents.Count(c => c.IsRead);
            var totalContentsCount = contents.Count;

            var progressPercentage = (readContentsCount / (double)totalContentsCount) * 100;

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var enrollment = await _unitOfWork.Enrollments.FindByExpress(e => e.ApplicationUserId == userId && e.CourseId == courseId);
            enrollment.ProgressPercentage = (int?)progressPercentage;
            await _unitOfWork.Complete();

            return Json(progressPercentage);
        }


        #endregion
    }
}
