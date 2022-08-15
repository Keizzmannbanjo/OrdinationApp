using Microsoft.EntityFrameworkCore;
using OrdinationApp.Data;
using OrdinationApp.Models;
using OrdinationApp.ViewModels;

namespace OrdinationApp.Services.ModelServices
{
    public class OrdinationBillServices : IOrdinationBillServices
    {
        private readonly ApplicationDbContext _db;

        public OrdinationBillServices(ApplicationDbContext db)
        {
            this._db = db;
        }

        public bool AddOrdinationBill(AddBillViewModel bill)
        {
            var newBill = new OrdinationBill { RankTitle = bill.RankTitle, OrdinationFee = bill.OrdinationFee, TrainingFee = bill.TrainingFee, IronStaffPrice = bill.IronStaffPrice, WoodenStaffPrice = bill.WoodenStaffPrice };
            var checkBillExist = _db.OrdinationBills.Any(o => o.RankTitle == newBill.RankTitle);
            if (!checkBillExist)
            {
                _db.OrdinationBills.Add(newBill);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public OrdinationBill GetOrdinationBill(int id)
        {
            return _db.OrdinationBills.First(o => o.Id == id);
        }

        public IEnumerable<OrdinationBill> GetOrdinationBills()
        {
            return _db.OrdinationBills.Include(o => o.Rank);
        }

        public void UpdateOrdinationBill(OrdinationBill bill)
        {
            _db.OrdinationBills.Update(bill);
            _db.SaveChanges();
        }
    }
}
