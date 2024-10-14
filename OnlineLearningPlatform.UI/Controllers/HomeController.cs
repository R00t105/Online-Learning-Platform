using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.UI.Models;
using System.Diagnostics;

namespace OnlineLearningPlatform.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _iUnitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork IUnitOfWork)
        {
            _logger = logger;
            _iUnitOfWork = IUnitOfWork;
        }

        public IActionResult Index()
        {
            ViewBag.Tracks = _iUnitOfWork.Tracks.GetAllAsync().Result;
            ViewBag.Courses = _iUnitOfWork.Courses.GetAllAsync().Result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
