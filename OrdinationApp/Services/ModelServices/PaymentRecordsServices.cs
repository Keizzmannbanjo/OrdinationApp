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
        public bool CreatePaymentRecord(Member member)
        {
            var newPayment = new PaymentRecord
            {
                MemberId = member.Id,
                RankTitle = member.TargetRankTitle,
                TallyNo = ControlNumberGenerator.Generate(),
                MembershipId=member.MemberShipId
            };
            var checkIfExist = _db.PaymentRecords.Any(p => p.MembershipId == member.MemberShipId && p.PaymentYear == member.OrdinationYear);
            if (checkIfExist)
            {
                return false;
            }
            _db.PaymentRecords.Add(newPayment);
            _db.SaveChanges();
            return true;
        }

        public PaymentRecord GetPaymentRecord(int memberId)
        {
            return _db.PaymentRecords.FirstOrDefault(p => p.MemberId == memberId);
        }
    }
}
