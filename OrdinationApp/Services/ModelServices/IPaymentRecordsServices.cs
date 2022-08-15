using OrdinationApp.Models;

namespace OrdinationApp.Services.ModelServices
{
    public interface IPaymentRecordsServices
    {
        void CreatePaymentRecord(Member member);

        PaymentRecord GetPaymentRecord(int memberId);
    }
}
