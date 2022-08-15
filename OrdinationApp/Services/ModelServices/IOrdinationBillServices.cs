using OrdinationApp.Models;
using OrdinationApp.ViewModels;

namespace OrdinationApp.Services.ModelServices
{
    public interface IOrdinationBillServices
    {
        IEnumerable<OrdinationBill> GetOrdinationBills();

        bool AddOrdinationBill(AddBillViewModel bill);

        OrdinationBill GetOrdinationBill(int id);

        void UpdateOrdinationBill(OrdinationBill bill);
    }
}
