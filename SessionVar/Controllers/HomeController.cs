using Microsoft.AspNetCore.Mvc;
using SessionVar.Models;
using System.Diagnostics;

namespace SessionVar.Controllers
{
    public class HomeController:Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
          
        }

        public IActionResult Index()
        {
            ViewBag.User= HttpContext.Session.GetString("User");
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