using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.Countries;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Services.Common;
using eSusInsurers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Services.Implementations;

public class CountriesService(
    IUnitOfWork unitOfWork,
    IMapper mapper
) : ICountriesService
{
    public async Task<List<RegionModel>?> GetRegions(long countryId, CancellationToken cancellationToken)
    {
        Dictionary<string, Models.Common.Filter> filters = RegionFilterById(countryId);
        Expression<Func<Region, bool>> predicate = ExpressionBuilder<Region>.BuildFilterExpression(filters);
        var query = unitOfWork.RegionsRepository.GetAll()
            .Where(predicate)
            .ProjectTo<RegionModel>(mapper.ConfigurationProvider);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<List<DistrictModel>?> GetDistricts(long countryId, long regionId,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Models.Common.Filter> filters = DistrictFilterById(countryId, regionId);
        Expression<Func<District, bool>> predicate = ExpressionBuilder<District>.BuildFilterExpression(filters);

        var query = unitOfWork.DistrictRepository.GetAll(new[] { "Region" })
            .Where(predicate)
            .ProjectTo<DistrictModel>(mapper.ConfigurationProvider);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<List<SubCountiesModel>?> GetSubcounties(long countryId, long regionId, long districtId,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Models.Common.Filter> filters = SubcountyFilterById(countryId, regionId, districtId);
        Expression<Func<SubCounty, bool>> predicate = ExpressionBuilder<SubCounty>.BuildFilterExpression(filters);

        var query = unitOfWork.SubcountiesRepository
            .GetAll(new[] { "District", "District.Region" })
            .Where(predicate)
            .ProjectTo<SubCountiesModel>(mapper.ConfigurationProvider);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<List<ParishModel>?> GetParishes(long countryId, long regionId, long districtId, long subCountyId,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Models.Common.Filter> filters =
            ParishFilterById(countryId, regionId, districtId, subCountyId);
        Expression<Func<Parish, bool>> predicate = ExpressionBuilder<Parish>.BuildFilterExpression(filters);

        var query = unitOfWork.ParishRepository
            .GetAll(new[] { "SubCounty", "SubCounty.District", "SubCounty.District.Region" })
            .Where(predicate)
            .ProjectTo<ParishModel>(mapper.ConfigurationProvider);

        return await query.ToListAsync(cancellationToken);
    }

    private static Dictionary<string, Models.Common.Filter> RegionFilterById(long countryId)
    {
        var filters = new Dictionary<string, Models.Common.Filter>();

        Filters.AddFilterIfValueGreaterThanZero(filters, countryId, "CountryId", SearchOperationEnum.Equal);

        return filters;
    }

    private static Dictionary<string, Models.Common.Filter> DistrictFilterById(long countryId, long regionId)
    {
        var filters = new Dictionary<string, Models.Common.Filter>();

        Filters.AddFilterIfValueGreaterThanZero(filters, countryId, "Region.CountryId", SearchOperationEnum.Equal);

        Filters.AddFilterIfValueGreaterThanZero(filters, regionId, "RegionId", SearchOperationEnum.Equal);

        return filters;
    }

    private static Dictionary<string, Models.Common.Filter> SubcountyFilterById(long countryId, long regionId,
        long districtId)
    {
        var filters = new Dictionary<string, Models.Common.Filter>();

        Filters.AddFilterIfValueGreaterThanZero(filters, countryId, "District.Region.CountryId",
            SearchOperationEnum.Equal);

        Filters.AddFilterIfValueGreaterThanZero(filters, regionId, "District.RegionId", SearchOperationEnum.Equal);

        Filters.AddFilterIfValueGreaterThanZero(filters, districtId, "DistrictId", SearchOperationEnum.Equal);

        return filters;
    }

    private static Dictionary<string, Models.Common.Filter> ParishFilterById(long countryId, long regionId,
        long districtId, long subCountyId)
    {
        var filters = new Dictionary<string, Models.Common.Filter>();

        Filters.AddFilterIfValueGreaterThanZero(filters, countryId, "SubCounty.District.Region.CountryId",
            SearchOperationEnum.Equal);

        Filters.AddFilterIfValueGreaterThanZero(filters, regionId, "SubCounty.District.RegionId",
            SearchOperationEnum.Equal);

        Filters.AddFilterIfValueGreaterThanZero(filters, districtId, "SubCounty.DistrictId", SearchOperationEnum.Equal);

        Filters.AddFilterIfValueGreaterThanZero(filters, subCountyId, "SubCountyId", SearchOperationEnum.Equal);

        return filters;
    }
}