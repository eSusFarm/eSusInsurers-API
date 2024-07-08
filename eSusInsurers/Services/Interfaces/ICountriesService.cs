using eSusInsurers.Models.Countries;
using eSusInsurers.Models.Users.GetUsers;

namespace eSusInsurers.Services.Interfaces
{
    public interface ICountriesService
    {
        Task<List<RegionModel>?> GetRegions(long country_id, CancellationToken cancellationToken);
        Task<List<DistrictModel>?> GetDistricts(long region_id, CancellationToken cancellationToken);
        Task<List<SubCountiesModel>?> GetSubcounties(long region_id, CancellationToken cancellationToken); 
    }
}
