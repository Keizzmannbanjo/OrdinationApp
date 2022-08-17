using OrdinationApp.Models;

namespace OrdinationApp.Services.ModelServices
{
    public interface IPaymentRecordsServices
    {
        bool CreatePaymentRecord(Member member);

        PaymentRecord GetPaymentRecord(int memberId);
    }
}
