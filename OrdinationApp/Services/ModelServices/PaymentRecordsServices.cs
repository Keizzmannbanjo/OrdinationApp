using OrdinationApp.Data;
using OrdinationApp.Models;


namespace OrdinationApp.Services.ModelServices
{
    public class PaymentRecordsServices : IPaymentRecordsServices
    {
        private readonly ApplicationDbContext _db;

        public PaymentRecordsServices(ApplicationDbContext db)
        {
            this._db = db;
        }
        public void CreatePaymentRecord(Member member)
        {
            var newPayment = new PaymentRecord
            {
                MemberId = member.Id,
                RankTitle = member.TargetRankTitle
            };
            _db.PaymentRecords.Add(newPayment);
            _db.SaveChanges();
        }

        public PaymentRecord GetPaymentRecord(int memberId)
        {
            return _db.PaymentRecords.FirstOrDefault(p => p.MemberId == memberId);
        }
    }
}
