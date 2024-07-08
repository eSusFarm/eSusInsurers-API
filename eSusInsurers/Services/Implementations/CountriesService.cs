using AutoMapper;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.Countries;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Services.Common;
using eSusInsurers.Services.Interfaces;
using System.Linq.Expressions;
using eSusInsurers.Domain.Entities;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;


namespace eSusInsurers.Services.Implementations
{
    public class CountriesService(IUnitOfWork unitOfWork,
                             IMapper mapper
                            ) : ICountriesService
    {
        public async Task<List<RegionModel>?> GetRegions(long country_id, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = RegionFilterById(country_id);
            Expression<Func<Region, bool>> predicate = ExpressionBuilder<Region>.BuildFilterExpression(filters);

            var query = unitOfWork.CountriesRepository.GetAll()
               .Where(predicate)
               .OrderByDescending(x => x.Id)
               .ProjectTo<RegionModel>(mapper.ConfigurationProvider);

            return  await query.ToListAsync(cancellationToken);
        }

        public async Task <List<DistrictModel>?> GetDistricts(long region_id ,CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = DistrictFilterById(region_id);
            Expression<Func<District, bool>> predicate = ExpressionBuilder<District>.BuildFilterExpression(filters);

            var query = unitOfWork.DistrictRepository.GetAll()
               .Where(predicate)
               .OrderByDescending(x => x.Id)
               .ProjectTo<DistrictModel>(mapper.ConfigurationProvider);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<SubCountiesModel>?> GetSubcounties(long district_id, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = SubcountyFilterById(district_id);
            Expression<Func<SubCounty, bool>> predicate = ExpressionBuilder<SubCounty>.BuildFilterExpression(filters);

            var query = unitOfWork.SubcountiesRepository.GetAll()
               .Where(predicate)
               .OrderByDescending(x => x.Id)
               .ProjectTo<SubCountiesModel>(mapper.ConfigurationProvider);

            return await query.ToListAsync(cancellationToken);
        }

        private static Dictionary<string, Models.Common.Filter> RegionFilterById(long country_id)
        {
            var filters = new Dictionary<string, Models.Common.Filter>();

            Filters.AddFilterIfValueGreaterThanZero(filters, country_id, "Id", SearchOperationEnum.Equal);

            return filters;
        }

        private static Dictionary<string, Models.Common.Filter> DistrictFilterById(long region_id)
        {
            var filters = new Dictionary<string, Models.Common.Filter>();

            Filters.AddFilterIfValueGreaterThanZero(filters, region_id, "RegionId", SearchOperationEnum.Equal);

            return filters;
        }

        private static Dictionary<string, Models.Common.Filter> SubcountyFilterById(long district_id)
        {
            var filters = new Dictionary<string, Models.Common.Filter>();

            Filters.AddFilterIfValueGreaterThanZero(filters, district_id, "DistrictId", SearchOperationEnum.Equal);

            return filters;
        }

    }
}
