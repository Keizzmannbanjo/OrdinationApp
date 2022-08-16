using Microsoft.AspNetCore.Mvc;

namespace OrdinationApp.Controllers
{
    public class PaymentsController : Controller
    {
        public IActionResult UpdatePayments()
        {
            return View();
        }

        public IActionResult ViewPaymentsRecords()
        {
            return View();
        }
    }
}
