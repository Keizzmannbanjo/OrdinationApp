using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrdinationApp.Models;
using OrdinationApp.ViewModels;
using OrdinationApp.Services.ModelServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrdinationApp.ViewModels.SessionViewModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace OrdinationApp.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IMemberServices memberServices;
        private readonly IPaymentRecordsServices paymentRecordsServices;
        private readonly IRankServices rankServices;
        private readonly IOrdinationBillServices ordinationBillServices;
        private readonly ICMCServices cmcServices;
        private readonly IProvinceServices provinceServices;

        public PaymentsController(IMemberServices memberServices, IPaymentRecordsServices paymentRecordsServices, IRankServices rankServices, IOrdinationBillServices ordinationBillServices, ICMCServices cmcServices, IProvinceServices provinceServices)
        {
            this.memberServices = memberServices;
            this.paymentRecordsServices = paymentRecordsServices;
            this.rankServices = rankServices;
            this.ordinationBillServices = ordinationBillServices;
            this.cmcServices = cmcServices;
            this.provinceServices = provinceServices;
        }
        public IActionResult UpdatePayments()
        {
            var ranksCount = rankServices.GetRanks().Count() - 2;
            var billsCount = ordinationBillServices.GetOrdinationBills().Count();
            if (ranksCount != billsCount)
            {
                return RedirectToAction("PaymentError", "Payments");
            }
            var checkSessionExist = HttpContext.Session.Get("MembersToVerify");
            if (checkSessionExist != null)
            {
                List<Member> membersToVerify = JsonConvert.DeserializeObject<List<Member>>(HttpContext.Session.GetString("MembersToVerify"));
                return View(membersToVerify);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FillMember(string membershipId)
        {
            var member = memberServices.GetMember(membershipId);
            var checkIfSessionExist = HttpContext.Session.Get("MembersToVerify");
            if (checkIfSessionExist != null)
            {
                var membersToVerify = JsonConvert.DeserializeObject<List<Member>>(HttpContext.Session.GetString("MembersToVerify"));
                membersToVerify.Add(member);
                HttpContext.Session.SetString("MembersToVerify", JsonConvert.SerializeObject(membersToVerify));
            }
            else
            {
                List<Member> membersToVerify = new List<Member>();
                membersToVerify.Add(member);
                HttpContext.Session.SetString("MembersToVerify", JsonConvert.SerializeObject(membersToVerify));
            }


            return RedirectToAction("UpdatePayments", "Payments");
        }

        public IActionResult SavePayments()
        {
            List<Member> membersToVerify = JsonConvert.DeserializeObject<List<Member>>(HttpContext.Session.GetString("MembersToVerify"));
            foreach (var member in membersToVerify)
            {
                var created = paymentRecordsServices.CreatePaymentRecord(member);
                if (created)
                {
                    memberServices.ChangePaymentStatus(member.Id);
                }
            }
            HttpContext.Session.Remove("MembersToVerify");
            return RedirectToAction("Index", "Approvals");
        }

        public IActionResult PaymentError()
        {
            return View();
        }

        public IActionResult ViewPaymentRecords()
        {
            var paymentRecords = paymentRecordsServices.GetPaymentRecords();
            var ranks = rankServices.GetRanks().OrderBy(r => r.Title);
            var viewModel = new FullPaymentRecordViewModel { Payments = new List<SinglePaymentRecordViewModel>() };
            foreach (var rank in ranks)
            {
                int noOfPaidForRank = 0;
                OrdinationBill? rankBill = null;
                try
                {
                    noOfPaidForRank = paymentRecordsServices.GetPaymentRecords().Where(p => p.RankTitle == rank.Title).Count();
                }
                catch (Exception ex)
                {
                    noOfPaidForRank = 0;
                }
                var noOFApprovedForRank = memberServices.GetMembers().Where(m => m.TargetRankTitle == rank.Title).Count();
                try
                {
                    rankBill = ordinationBillServices.GetOrdinationBill(rank.Title);
                }
                catch (Exception ex)
                {
                    continue;
                }
                var rankCost = rankBill.OrdinationFee + rankBill.TrainingFee + rankBill.WoodenStaffPrice + rankBill.IronStaffPrice;
                var totalCostForRank = rankCost * noOfPaidForRank;
                var rankViewModel = new SinglePaymentRecordViewModel { RankTitle = rank.Title, NoOfApproved = noOFApprovedForRank, NoOfPaid = noOfPaidForRank, TotalPaid = totalCostForRank };
                viewModel.Payments.Add(rankViewModel);
                viewModel.TotalApproved += rankViewModel.NoOfApproved;
                viewModel.TotalPaid += rankViewModel.NoOfPaid;
                viewModel.TotalGenerated += rankViewModel.TotalPaid;
            }
            return View(viewModel);
        }


        public IActionResult ViewPaymentReport(string cmcName, string provinceName, int? year)
        {
            var model = PopulatePaymentReportViewModel(new PaymentReportViewModel());
            var allPaymentRecords = paymentRecordsServices.GetPaymentRecords();
            if (!string.IsNullOrEmpty(cmcName))
            {
                allPaymentRecords = allPaymentRecords.Where(p => p.Member.Province.CMC.Name == cmcName);
            }
            if (!string.IsNullOrEmpty(provinceName))
            {
                allPaymentRecords = allPaymentRecords.Where(p => p.Member.ProvinceName == provinceName);
            }
            if (year != null || year <= 0)
            {
                allPaymentRecords = allPaymentRecords.Where(p => p.Member.OrdinationYear == year);
            }
            else if (year == null)
            {
                allPaymentRecords = allPaymentRecords.Where(p => p.Member.OrdinationYear == DateTime.Now.Year);

            }
            model.paymentRecords = allPaymentRecords;
            var checkIfExportExist = HttpContext.Session.Get("Export");
            if (allPaymentRecords != null)
            {
                var sessionModel = new PaymentsView { Ids = new List<int>() };
                foreach (var record in allPaymentRecords)
                {
                    sessionModel.Ids.Add(record.Id);
                }
                HttpContext.Session.SetString("Export", JsonConvert.SerializeObject(sessionModel));
            }
            return View(PopulatePaymentReportViewModel(model));
        }

        public IActionResult ExportReport()
        {
            var checkIfExportExist = HttpContext.Session.Get("Export");
            if (checkIfExportExist != null)
            {
                var paymentsId = JsonConvert.DeserializeObject<PaymentsView>(HttpContext.Session.GetString("Export"));
                List<PaymentRecord> paymentsToExport = new List<PaymentRecord>();
                foreach (var id in paymentsId.Ids)
                {
                    var record = paymentRecordsServices.GetPaymentRecord(id);
                    paymentsToExport.Add(record);
                }
                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var pkg = new ExcelPackage(stream))
                {
                    var worksheet = pkg.Workbook.Worksheets.Add("Tally Datas");
                    using (var r = worksheet.Cells["A1:F1"])
                    {
                        r.Merge = true;
                        r.Style.Font.Bold = true;
                        r.Value = "List Of Tally Controls";
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    using (var r = worksheet.Cells["A2:F2"])
                    {
                        r.Style.Font.Bold = true;
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    worksheet.Cells["A2"].Value = "Membership ID";
                    worksheet.Cells["B2"].Value = "Current Rank";
                    worksheet.Cells["C2"].Value = "Full Name";
                    worksheet.Cells["D2"].Value = "Target Rank";
                    worksheet.Cells["E2"].Value = "CMC";
                    worksheet.Cells["F2"].Value = "Tally No";
                    var startRow = 3;
                    foreach (var record in paymentsToExport)
                    {
                        worksheet.Cells[startRow, 1].Value = record.Member.MemberShipId;
                        worksheet.Cells[startRow, 2].Value = record.Member.CurrentRankTitle;
                        worksheet.Cells[startRow, 3].Value = record.Member.Surname + " " + record.Member.FirstName + " " + record.Member.Othername;
                        worksheet.Cells[startRow, 4].Value = record.Member.TargetRankTitle;
                        worksheet.Cells[startRow, 5].Value = record.Member.Province.CmcName;
                        worksheet.Cells[startRow, 6].Value = record.TallyNo;
                        startRow++;
                    }

                    pkg.Workbook.Properties.Title = "List Of Members Tally No";

                    pkg.Save();
                }
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tally Controls.xlsx");
            }
            else
            {
                return RedirectToAction("ViewPaymentReport");
            }
        }

        private PaymentReportViewModel PopulatePaymentReportViewModel(PaymentReportViewModel model)
        {
            var cmcs = cmcServices.GetCMCs();
            var provinces = provinceServices.GetProvinces();

            var cmcList = new List<SelectListItem>();
            var provinceList = new List<SelectListItem>();
            foreach (var province in provinces)
            {
                var item = new SelectListItem { Text = province.Name, Value = province.Name };
                provinceList.Add(item);
            }
            foreach (var cmc in cmcs)
            {
                var item = new SelectListItem { Text = cmc.Name, Value = cmc.Name };
                cmcList.Add(item);
            }
            model.provinceList = provinceList;
            model.cmcList = cmcList;
            return model;
        }
    }
}
