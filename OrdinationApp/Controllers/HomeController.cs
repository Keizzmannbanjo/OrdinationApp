using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdinationApp.Models;
using OrdinationApp.ViewModels;
using System.Diagnostics;

namespace OrdinationApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<HomepageUserViewModel> Teams = new List<HomepageUserViewModel> { new HomepageUserViewModel("Michael Banjo", "Coder"), new HomepageUserViewModel("Yetunde Banjo", "Coder"), new HomepageUserViewModel("Samson Bodunrin", "Admin"), new HomepageUserViewModel("Kolade Banjo", "Admin"), new HomepageUserViewModel("Tunji Bello", "Coder") };
            return View(Teams);
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