using Microsoft.EntityFrameworkCore;
using OrdinationApp.Data;
using OrdinationApp.Models;

namespace OrdinationApp.Services.ModelServices
{
    public class CMCServices : ICMCServices
    {
        private readonly ApplicationDbContext _db;
        public CMCServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<CMC> GetCMCs()
        {
            return _db.CMCs.Include(c => c.Provinces);
        }
    }
}
