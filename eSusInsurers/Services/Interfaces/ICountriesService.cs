using eSusInsurers.Models.Countries;
using eSusInsurers.Models.Users.GetUsers;

namespace eSusInsurers.Services.Interfaces
{
    public interface ICountriesService
    {
        Task<List<RegionModel>?> GetRegions(long countryId, CancellationToken cancellationToken);
        Task<List<DistrictModel>?> GetDistricts(long countryId, long regionId, CancellationToken cancellationToken);
        Task<List<SubCountiesModel>?> GetSubcounties(long countryId, long regionId, long districtId, CancellationToken cancellationToken);
        Task<List<ParishModel>?> GetParishes(long countryId, long regionId, long districtId, long subCountyId, CancellationToken cancellationToken);
    }
}
