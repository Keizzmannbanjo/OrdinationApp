using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrdinationApp.Models;
using OrdinationApp.Services.ModelServices;

namespace OrdinationApp.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IMemberServices memberServices;
        private readonly IPaymentRecordsServices paymentRecordsServices;
        private readonly IRankServices rankServices;
        private readonly IOrdinationBillServices ordinationBillServices;


        public PaymentsController(IMemberServices memberServices, IPaymentRecordsServices paymentRecordsServices, IRankServices rankServices, IOrdinationBillServices ordinationBillServices)
        {
            this.memberServices = memberServices;
            this.paymentRecordsServices = paymentRecordsServices;
            this.rankServices = rankServices;
            this.ordinationBillServices = ordinationBillServices;
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
    }
}
