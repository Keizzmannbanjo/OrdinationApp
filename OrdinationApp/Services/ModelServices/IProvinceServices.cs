using OrdinationApp.Models;

namespace OrdinationApp.Services.ModelServices
{
    public interface IProvinceServices
    {
        IEnumerable<Province> GetProvinces();
        bool AddProvince(Province province);
    }
}
