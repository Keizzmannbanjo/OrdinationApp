using OrdinationApp.Models;

namespace OrdinationApp.Services.ModelServices
{
    public interface IRankServices
    {
        IEnumerable<Rank> GetRanks();
    }
}
