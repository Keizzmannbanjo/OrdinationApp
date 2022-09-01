namespace OrdinationApp.ViewModels
{
    public class FullPaymentRecordViewModel
    {
        public string searchParameter { get; set; } = "All Payments";

        public List<SinglePaymentRecordViewModel> Payments { get; set; }

        public int TotalApproved { get; set; }
        public int TotalPaid { get; set; }

        public decimal TotalGenerated { get; set; }
    }
}
