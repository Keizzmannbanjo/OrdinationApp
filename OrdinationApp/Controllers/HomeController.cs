using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<TrackerUser> userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<TrackerUser> userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = userManager.Users.ToList();
            List<ManageUserViewModel> model = new List<ManageUserViewModel>();
            foreach (var user in users)
            {
                var newUser = new ManageUserViewModel();
                newUser.user = user;
                var userRoles = await userManager.GetRolesAsync(user);
                foreach(var role in userRoles)
                {
                    newUser.Role = role;
                }
                model.Add(newUser);
            }
            return View(model);
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