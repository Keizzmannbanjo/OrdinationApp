using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrdinationApp.Models;
using OrdinationApp.Services.ModelServices;
using OrdinationApp.ViewModels;

namespace OrdinationApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<TrackerUser> userManager;
        private readonly IRankServices rankServices;
        private readonly IProvinceServices provinceServices;
        private readonly RoleManager<IdentityRole> roleManager;
        public readonly SignInManager<TrackerUser> signInManager;

        public AdminController(SignInManager<TrackerUser> signInManager, UserManager<TrackerUser> userManager, IRankServices rankServices, IProvinceServices provinceServices, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.rankServices = rankServices;
            this.provinceServices = provinceServices;
            this.roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username and password is incorrect");
                    return View(model);
                }
            }
            return View(model);
        }

        
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        //[Authorize(Roles = "Admin")]

        public IActionResult Register()
        {
            var model = PopupateRegisterUserViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new TrackerUser { UserName = model.Username, Email = model.Email, FirstName = model.FirstName, Surname = model.Surname, LastName = model.LastName, Rank = model.RankTitle, Province = model.ProvinceName };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var role = await roleManager.FindByIdAsync(model.Role);
                    if (role != null)
                    {
                        var roleResult = await userManager.AddToRoleAsync(user, role.ToString());
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    var mol = PopupateRegisterUserViewModel(model);
                    return View(mol);
                }
            }
            var newModel = PopupateRegisterUserViewModel(model);
            return View(newModel);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private RegisterUserViewModel PopupateRegisterUserViewModel()
        {
            var ranks = rankServices.GetRanks();
            var provinces = provinceServices.GetProvinces();
            IEnumerable<IdentityRole> roles = roleManager.Roles;
            var rankList = new List<SelectListItem>();
            var provinceList = new List<SelectListItem>();
            var roleList = new List<SelectListItem>();
            foreach (var rank in ranks)
            {
                var item = new SelectListItem { Text = rank.Title, Value = rank.Title };
                rankList.Add(item);
            }
            foreach (var province in provinces)
            {
                var item = new SelectListItem { Text = province.Name, Value = province.Name };
                provinceList.Add(item);
            }
            foreach (var role in roles)
            {
                var item = new SelectListItem { Text = role.Name, Value = role.Id };
                roleList.Add(item);
            }
            var model = new RegisterUserViewModel { ProvinceList = provinceList, RankList = rankList, RoleList=roleList };
            return model;
        }
        private  RegisterUserViewModel PopupateRegisterUserViewModel(RegisterUserViewModel model)
        {
            var ranks = rankServices.GetRanks();
            var provinces = provinceServices.GetProvinces();
            IEnumerable<IdentityRole> roles = roleManager.Roles;
            var rankList = new List<SelectListItem>();
            var provinceList = new List<SelectListItem>();
            var roleList = new List<SelectListItem>();
            foreach (var rank in ranks)
            {
                var item = new SelectListItem { Text = rank.Title, Value = rank.Title };
                rankList.Add(item);
            }
            foreach (var branch in provinces)
            {
                var item = new SelectListItem { Text = branch.Name, Value = branch.Name };
                provinceList.Add(item);
            }
            foreach (var role in roles)
            {
                var item = new SelectListItem { Text = role.Name, Value = role.Id };
                roleList.Add(item);
            }
            model.ProvinceList = provinceList;
            model.RankList = rankList;
            model.RoleList = roleList;
            return model;
        }
    }
}
