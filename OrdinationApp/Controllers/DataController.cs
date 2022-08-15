using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OrdinationApp.Models;
using OrdinationApp.Services.ModelServices;
using OrdinationApp.ViewModels;

namespace OrdinationApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DataController : Controller
    {
        private IProvinceServices _provinceServices;
        private ICMCServices _cmcServices;
        private IOrdinationBillServices _ordinationBillServices;
        private IRankServices _rankServices;
        public DataController(IProvinceServices provinceServices, ICMCServices cMCServices, IOrdinationBillServices ordinationBillServices, IRankServices rankServices)
        {
            _provinceServices = provinceServices;
            _cmcServices = cMCServices;
            _ordinationBillServices = ordinationBillServices;
            _rankServices = rankServices;
        }


        // Codes to Manage Provinces Here
        [HttpGet]
        public IActionResult ManageProvinces()
        {
            var newModel = CreateManageProvincesViewModel();
            return View(newModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageProvinces(ManageProvincesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newProvince = new Province { Name = model.NewProvinceName, CmcName = model.CmcName };
                var succeeded = _provinceServices.AddProvince(newProvince);
                if (succeeded)
                {
                    return RedirectToAction(actionName: "ManageProvinces");
                }
                else
                {
                    return View(CreateManageProvincesViewModel(model));
                }
                
            }
            return View(CreateManageProvincesViewModel(model));
        }

        public IActionResult BulkUploadProvinces()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BulkUploadProvinces(BulkUploadProvincesViewModel model)
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
                            for (var row = 2; row < rowCount + 1; row++)
                            {
                                var provinceName = worksheet.Cells[row, 2].Value.ToString();
                                var cmcName = worksheet.Cells[row, 3].Value.ToString();
                                var newProvince = new Province
                                {
                                    Name = provinceName,
                                    CmcName = cmcName
                                };
                                var succeeded = _provinceServices.AddProvince(newProvince);
                                if (!succeeded)
                                {
                                    errorMessage += $"Province ${newProvince.Name} alread exist";
                                }

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    if (errorMessage.Length <= 0)
                    {
                        return RedirectToAction("ManageProvinces");
                    }
                    else
                    {
                        ModelState.AddModelError("Exists", errorMessage);
                        return View(model);
                    }
                }
            }
            return View(model);
        }


        // Codes to Manage Branches Here
        //public IActionResult ManageBranches()
        //{
        //    var model = CreateManageBranchesViewModel();
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult ManageBranches(ManageBranchesViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _branchServices.AddBranch(model.ProvinceName, model.NewBranchName);
        //        return RedirectToAction(actionName: "ManageBranches");
        //    }
        //    return View(CreateManageBranchesViewModel(model));
        //}

        // Codes to Manage Ordination Bills Here
        public IActionResult ManageBills()
        {
            var model = new ManageBillsViewModel { Bills = _ordinationBillServices.GetOrdinationBills() };
            return View(model);
        }

        public IActionResult AddBill()
        {
            var model = PopulateAddBillViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBill(AddBillViewModel model)
        {
            var success = _ordinationBillServices.AddOrdinationBill(model);
            if (success)
            {
                return RedirectToAction(actionName: "ManageBills");
            }
            else
            {
                ModelState.AddModelError("", "Sorry, Bill Already Exist. Go to List to Update Bill Details");
                var newModel = PopulateAddBillViewModel(model);
                return View(newModel);
            }

        }

        public IActionResult EditBill(int id)
        {
            var bill = _ordinationBillServices.GetOrdinationBill(id);
            var model = PopulateEditBillViewModel(bill);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBill(EditBillViewModel model)
        {
            var bill = _ordinationBillServices.GetOrdinationBill(model.Id);
            if (bill != null)
            {
                if (ModelState.IsValid)
                {
                    bill.OrdinationFee = model.OrdinationFee;
                    bill.TrainingFee = model.TrainingFee;
                    bill.WoodenStaffPrice = model.WoodenStaffPrice;
                    bill.IronStaffPrice = model.IronStaffPrice;
                    _ordinationBillServices.UpdateOrdinationBill(bill);
                    return RedirectToAction("ManageBills");
                }
                return View(model);
            }
            return View(model);
        }


        // Private methods to populate ViewModels here
        private ManageProvincesViewModel CreateManageProvincesViewModel()
        {
            var provinces = _provinceServices.GetProvinces();
            var cmcs = _cmcServices.GetCMCs();
            List<SelectListItem> cmcList = new List<SelectListItem>();
            foreach (var cmc in cmcs)
            {
                SelectListItem item = new SelectListItem { Text = cmc.Name, Value = cmc.Name };
                cmcList.Add(item);
            }
            var model = new ManageProvincesViewModel { Provinces = provinces, CMCs = cmcList };
            return model;
        }

        //private ManageBranchesViewModel CreateManageBranchesViewModel()
        //{
        //    var branches = _branchServices.GetBranches();
        //    var provinces = _provinceServices.GetProvinces();
        //    List<SelectListItem> provinceList = new List<SelectListItem>();
        //    foreach (var province in provinces)
        //    {
        //        SelectListItem item = new SelectListItem { Text = province.Name, Value = province.Name };
        //        provinceList.Add(item);
        //    }
        //    var model = new ManageBranchesViewModel { ProvinceList = provinceList, Branches = branches };
        //    return model;
        //}

        //private ManageBranchesViewModel CreateManageBranchesViewModel(ManageBranchesViewModel model)
        //{
        //    var branches = _branchServices.GetBranches();
        //    var provinces = _provinceServices.GetProvinces();
        //    List<SelectListItem> provinceList = new List<SelectListItem>();
        //    foreach (var province in provinces)
        //    {
        //        SelectListItem item = new SelectListItem { Text = province.Name, Value = province.Name };
        //        provinceList.Add(item);
        //    }
        //    var newModel = model;
        //    newModel.ProvinceList = provinceList;
        //    newModel.Branches = branches;
        //    return newModel;
        //}

        private ManageProvincesViewModel CreateManageProvincesViewModel(ManageProvincesViewModel model)
        {
            var provinces = _provinceServices.GetProvinces();
            var cmcs = _cmcServices.GetCMCs();
            List<SelectListItem> cmcList = new List<SelectListItem>();
            foreach (var cmc in cmcs)
            {
                SelectListItem item = new SelectListItem { Text = cmc.Name, Value = cmc.Name };
                cmcList.Add(item);
            }
            var newModel = model;
            newModel.Provinces = provinces;
            newModel.CMCs = cmcList;
            return model;
        }

        private AddBillViewModel PopulateAddBillViewModel()
        {
            var ranks = _rankServices.GetRanks();
            List<SelectListItem> rankList = new List<SelectListItem>();
            foreach (var rank in ranks)
            {
                var item = new SelectListItem { Text = rank.Title, Value = rank.Title };
                rankList.Add(item);
            }
            var model = new AddBillViewModel { RankList = rankList };
            return model;
        }

        private AddBillViewModel PopulateAddBillViewModel(AddBillViewModel model)
        {
            var ranks = _rankServices.GetRanks();
            List<SelectListItem> rankList = new List<SelectListItem>();
            foreach (var rank in ranks)
            {
                var item = new SelectListItem { Text = rank.Title, Value = rank.Title };
                rankList.Add(item);
            }
            model.RankList = rankList;
            return model;
        }

        private EditBillViewModel PopulateEditBillViewModel(OrdinationBill bill)
        {
            var model = new EditBillViewModel
            {
                Id = bill.Id,
                OrdinationFee = bill.OrdinationFee,
                TrainingFee = bill.TrainingFee,
                WoodenStaffPrice = bill.WoodenStaffPrice,
                IronStaffPrice = bill.IronStaffPrice,

            };
            return model;
        }
    }
}

