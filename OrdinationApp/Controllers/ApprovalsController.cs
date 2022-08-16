using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OrdinationApp.Models;
using OrdinationApp.Services.ModelServices;
using OrdinationApp.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace OrdinationApp.Controllers
{
    [Authorize]
    public class ApprovalsController : Controller
    {
        private IMemberServices _memberServices;
        private IProvinceServices _provinceServices;
        private IRankServices _rankServices;

        public ApprovalsController(IMemberServices memberServices, IProvinceServices provinceServices, IRankServices rankServices)
        {
            _memberServices = memberServices;
            _provinceServices = provinceServices;
            _rankServices = rankServices;
        }
        public IActionResult Index(string provinceFilter, string branchFilter, string rankFilter)
        {
            IEnumerable<Member> members = _memberServices.GetMembers();
            if (provinceFilter != null)
            {
                members = members.Where(m => m.ProvinceName == provinceFilter);
            }

            if (rankFilter != null)
            {
                members = members.Where(m => m.CurrentRankTitle == rankFilter);
            }
            var model = PopulateIndexViewModel(members);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult SingleUpload()
        {
            var model = PopulateSingleUploadViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult SingleUpload(SingleUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member newMember = new Member
                {
                    FirstName = model.FirstName,
                    Surname = model.Surname,
                    Othername = model.Othername,
                    CurrentRankYear = Convert.ToInt32(model.CurrentRankYear),
                    Gender = model.Gender,
                    ProvinceName = model.ProvinceName,
                    CurrentRankTitle = model.CurrentRankTitle,
                    TargetRankTitle = model.TargetRankTitle,
                };
                var succeeded = _memberServices.AddMember(newMember);
                if (succeeded)
                {
                    return RedirectToAction(actionName: "Index");
                }
                else
                {
                    ModelState.AddModelError("Exist", $"Member with names {newMember.Surname} {newMember.FirstName} of year {newMember.OrdinationYear} already exist, try again!");
                    return View(PopulateSingleUploadViewModel(model));
                }
            }
            return View(PopulateSingleUploadViewModel(model));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult BulkUpload()
        {
            var model = new BulkUploadViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult BulkUpload(BulkUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                string filename = model.upload.FileName;
                string errorMessage = "";
                if (!filename.Contains(".xlsx"))
                {
                    ModelState.AddModelError("upload", "File upload must be an excel file, try again!");
                    return View(model);
                }
                if (model.upload.Length > 0)
                {
                    var stream = model.upload.OpenReadStream();
                    try
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (var package = new ExcelPackage(stream))
                        {
                            var worksheet = package.Workbook.Worksheets.First();
                            var rowCount = worksheet.Dimension.Rows;
                            // Fix Exception error
                            for (var row = 4; row < rowCount+1; row++)
                            {
                                var membershidId = worksheet.Cells[row, 1].Value.ToString();
                                var surname = worksheet.Cells[row, 2].Value.ToString();
                                var firstName = worksheet.Cells[row, 3].Value.ToString();
                                var otherName = worksheet.Cells[row, 4].Value.ToString();
                                var province = worksheet.Cells[row, 5].Value.ToString();
                                var rankyear = worksheet.Cells[row, 6].Value;
                                var currentRank = worksheet.Cells[row, 7].Value.ToString();
                                var targetRank = worksheet.Cells[row, 8].Value.ToString();
                                var gender = worksheet.Cells[row, 9].Value.ToString();
                                var newApproval = new Member
                                {
                                    MemberShipId=membershidId,
                                    FirstName = firstName,
                                    Surname = surname,
                                    Othername = otherName,
                                    ProvinceName = province,
                                    CurrentRankTitle = currentRank,
                                    TargetRankTitle = targetRank,
                                    Gender = gender,
                                    CurrentRankYear = Convert.ToInt32(rankyear),
                                };
                                var succeeded = _memberServices.AddMember(newApproval);
                                if (!succeeded)
                                {
                                    errorMessage += $"Member with names {newApproval.Surname} {newApproval.FirstName} for year {newApproval.OrdinationYear} already exist";
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("upload", ex.Message);
                        return View(model);
                    }
                    if (errorMessage.Length <= 0)
                    {
                        return RedirectToAction(actionName: "Index");
                    }
                    else
                    {
                        ModelState.AddModelError("upload", errorMessage);
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        public IActionResult EditDetails(int id)
        {
            var member = _memberServices.GetMember(id);
            var model = PopulateEditDetailsViewModel(member);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDetails(EditDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var member = _memberServices.GetMember(model.Id);
                if (member != null)
                {
                    member.FirstName = model.FirstName;
                    member.Surname = model.Surname;
                    member.Othername = model.Othername;
                    member.ProvinceName = model.ProvinceName;
                    member.Gender = model.Gender;
                    member.CurrentRankTitle = model.CurrentRankTitle;
                    member.TargetRankTitle = model.TargetRankTitle;
                }
                _memberServices.UpdateMember(member);

                return RedirectToAction(actionName: "Index");

            }

            var newModel = PopulateEditDetailsViewModel(model);
            return View(newModel);
        }



        // Private View Model Populator methods here
        private IndexViewModel PopulateIndexViewModel(IEnumerable<Member> members)
        {
            IEnumerable<Province> provinces = _provinceServices.GetProvinces();
            IEnumerable<Rank> ranks = _rankServices.GetRanks();
            List<SelectListItem> rankList = new List<SelectListItem>();
            List<SelectListItem> branchList = new List<SelectListItem>();
            List<SelectListItem> provinceList = new List<SelectListItem>();
            foreach (var province in provinces)
            {
                var item = new SelectListItem { Text = province.Name, Value = province.Name };
                provinceList.Add(item);
            }

            foreach (var rank in ranks)
            {
                var item = new SelectListItem { Text = rank.Title, Value = rank.Title };
                rankList.Add(item);
            }
            return new IndexViewModel { rankList = rankList, provinceList = provinceList, members = members };
        }
        private EditDetailsViewModel PopulateEditDetailsViewModel(Member member)
        {
            var ranks = _rankServices.GetRanks();
            var provinces = _provinceServices.GetProvinces();
            var rankList = new List<SelectListItem>();
            var provinceList = new List<SelectListItem>();
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
            var result = new EditDetailsViewModel
            {
                FirstName = member.FirstName,
                Surname = member.Surname,
                Othername = member.Othername,
                Gender = member.Gender,
                ProvinceName = member.ProvinceName,
                RankList = rankList,
                ProvinceList = provinceList,
                CurrentRankYear = member.CurrentRankYear,
                CurrentRankTitle = member.CurrentRankTitle,
                TargetRankTitle = member.TargetRankTitle,

            };
            return result;
        }

        private EditDetailsViewModel PopulateEditDetailsViewModel(EditDetailsViewModel model)
        {
            var ranks = _rankServices.GetRanks();
            var provinces = _provinceServices.GetProvinces();
            var rankList = new List<SelectListItem>();
            var provinceList = new List<SelectListItem>();
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
            model.ProvinceList = provinceList;
            model.RankList = rankList;
            return model;
        }

        private SingleUploadViewModel PopulateSingleUploadViewModel()
        {
            IEnumerable<Rank> ranks = _rankServices.GetRanks();
            IEnumerable<Province> provinces = _provinceServices.GetProvinces();
            List<SelectListItem> rankList = new List<SelectListItem>();
            List<SelectListItem> provinceList = new List<SelectListItem>();
            foreach (Rank rank in ranks)
            {
                var item = new SelectListItem { Value = rank.Title, Text = rank.Title };
                rankList.Add(item);
            }
            foreach (Province province in provinces)
            {
                var item = new SelectListItem { Value = province.Name, Text = province.Name };
                provinceList.Add(item);
            }
            return new SingleUploadViewModel { ProvinceList = provinceList, RankList = rankList };
        }

        private SingleUploadViewModel PopulateSingleUploadViewModel(SingleUploadViewModel model)
        {
            IEnumerable<Rank> ranks = _rankServices.GetRanks();
            IEnumerable<Province> provinces = _provinceServices.GetProvinces();
            List<SelectListItem> rankList = new List<SelectListItem>();
            List<SelectListItem> provinceList = new List<SelectListItem>();
            foreach (Rank rank in ranks)
            {
                var item = new SelectListItem { Value = rank.Title, Text = rank.Title };
                rankList.Add(item);
            }
            foreach (Province province in provinces)
            {
                var item = new SelectListItem { Value = province.Name, Text = province.Name };
                provinceList.Add(item);
            }
            var newModel = model;
            newModel.RankList = rankList;
            newModel.ProvinceList = provinceList;
            return newModel;
        }
    }
}
