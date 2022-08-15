using Microsoft.EntityFrameworkCore;
using OrdinationApp.Data;
using OrdinationApp.Models;

namespace OrdinationApp.Services.ModelServices
{
    public class ProvinceServices : IProvinceServices
    {
        private readonly ApplicationDbContext _db;
        public ProvinceServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AddProvince(Province province)
        {
            var checkIfExist = _db.Provinces.Any(p => p.Name == province.Name);
            if (!checkIfExist)
            {
                _db.Provinces.Add(province);
                _db.SaveChanges();
                return true;
            }
            return false;
           
        }

        public IEnumerable<Province> GetProvinces()
        {
            return _db.Provinces.OrderBy(p => p.Name);
        }
    }
}
