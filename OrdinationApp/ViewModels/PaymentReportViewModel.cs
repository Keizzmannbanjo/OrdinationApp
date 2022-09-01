using Microsoft.AspNetCore.Mvc.Rendering;
using OrdinationApp.Models;

namespace OrdinationApp.ViewModels
{
    public class PaymentReportViewModel
    {

        public List<SelectListItem>? cmcList { get; set; }
        public List<SelectListItem>? provinceList { get; set; }

        public IEnumerable<PaymentRecord> paymentRecords { get; set; }
    }
}
