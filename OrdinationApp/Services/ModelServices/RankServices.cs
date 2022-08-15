using OrdinationApp.Data;
using OrdinationApp.Models;

namespace OrdinationApp.Services.ModelServices
{
    public class RankServices : IRankServices
    {
        private readonly ApplicationDbContext _db;
        public RankServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Rank> GetRanks()
        {
            return _db.Ranks;
        }
    }
}
