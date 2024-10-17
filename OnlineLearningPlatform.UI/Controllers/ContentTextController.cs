using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.UI.ViewModels;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class ContentTextController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContentTextController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }


        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Contents = await _unitOfWork.Contents.GetAllAsync();
            return View();
        }

        // POST: Content/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,SubTitle,Paragraph,ContentId")] ContentTextViewModel contenttextViewModel)
        {
            if (ModelState.IsValid)
            {
                ContentText content = new ContentText();
                content.Id = contenttextViewModel.Id;
                content.Title = contenttextViewModel.Title;
                content.SubTitle = contenttextViewModel.SubTitle;
                content.Paragraph = contenttextViewModel.Paragraph;
                content.ContentId = contenttextViewModel.ContentId;
                await _unitOfWork.ContentTexts.AddAsync(content);
                return RedirectToAction("ContentTexts", "Dashboard");
            }

            // Repopulate the Courses list when the ModelState is invalid
            ViewBag.Contents = await _unitOfWork.Contents.GetAllAsync();
            return View(contenttextViewModel);
        }
        #endregion


        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentText = await _unitOfWork.ContentTexts.GetByIdAsync(id.Value);
            if (contentText == null)
            {
                return NotFound();
            }
            ContentTextViewModel contenttextViewModel = new ContentTextViewModel() 
            { Id = contentText.Id,
                Title = contentText.Title,
                SubTitle = contentText.SubTitle, 
                Paragraph = contentText.Paragraph,
                ContentId= contentText.ContentId };
            ViewBag.Contents = await _unitOfWork.Contents.GetAllAsync();
            return View(contenttextViewModel);
        }



        // POST: Content/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,SubTitle,Paragraph,ContentId")] ContentTextViewModel contenttextViewModel)
        {
            if (id != contenttextViewModel.Id)
            {
                return NotFound();
            }
            ContentText contenttext = await _unitOfWork.ContentTexts.GetByIdAsync(id);
            contenttext.Title = contenttextViewModel.Title;
            contenttext.SubTitle = contenttextViewModel.SubTitle;
            contenttext.Paragraph = contenttextViewModel.Paragraph;
            contenttext.ContentId= contenttextViewModel.ContentId;

            if (ModelState.IsValid)
            {
               

                    await _unitOfWork.ContentTexts.UpdateAsync(contenttext);
                    await _unitOfWork.Complete();
                
                return RedirectToAction("ContentTexts", "Dashboard");
            }
            ViewBag.Contents = await _unitOfWork.Contents.GetAllAsync();
            return View(contenttextViewModel);
        }

        #endregion


        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentText = await _unitOfWork.ContentTexts.GetByIdAsync(id.Value);
            if (contentText == null)
            {
                return NotFound();
            }
            var content = await _unitOfWork.Contents.GetByIdAsync(contentText.ContentId);
            ViewBag.contentname = content.Title;
            return View("Delete", contentText);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentText = await _unitOfWork.ContentTexts.GetByIdAsync(id);
            if (contentText != null)
            {
                await _unitOfWork.ContentTexts.RemoveAsync(id);
                await _unitOfWork.Complete();
            }
            return RedirectToAction("ContentTexts", "Dashboard");
        }

        #endregion

    }
}
