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
        public async Task<List<RegionModel>?> GetRegions(long countryId, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = RegionFilterById(countryId);
            Expression<Func<Region, bool>> predicate = ExpressionBuilder<Region>.BuildFilterExpression(filters);

            var query = unitOfWork.CountriesRepository.GetAll()
               .Where(predicate)
               .ProjectTo<RegionModel>(mapper.ConfigurationProvider);

            return  await query.ToListAsync(cancellationToken);
        }

        public async Task <List<DistrictModel>?> GetDistricts(long countryId, long regionId, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = DistrictFilterById(countryId, regionId);
            Expression<Func<District, bool>> predicate = ExpressionBuilder<District>.BuildFilterExpression(filters);

            var query = unitOfWork.DistrictRepository.GetAll(new string[] { "Region" })
               .Where(predicate)
               .ProjectTo<DistrictModel>(mapper.ConfigurationProvider);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<DistrictModel>> GetDistrictsByFirstThreeCharacters(long countryId, long regionId,
            CancellationToken cancellationToken, String searchText)
        {
            List<DistrictModel> unfilteredDistricts = await GetDistricts(countryId, regionId, cancellationToken);
            if (unfilteredDistricts != null || unfilteredDistricts.Count > 0)
            {
                List<DistrictModel> filteredList = new List<DistrictModel>();
                for (int i = 0; i < unfilteredDistricts.Count; i++)
                {
                    if (unfilteredDistricts[i].DistrictName.ToLower().StartsWith(searchText.Substring(0, 3).ToLower()))
                    {
                        filteredList.Add(unfilteredDistricts[i]);
                    }
                }
                return filteredList;
            }
            return new List<DistrictModel>();
        }

        public async Task<List<SubCountiesModel>?> GetSubcounties(long countryId, long regionId, long districtId, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = SubcountyFilterById(countryId, regionId, districtId);
            Expression<Func<SubCounty, bool>> predicate = ExpressionBuilder<SubCounty>.BuildFilterExpression(filters);

            var query = unitOfWork.SubcountiesRepository
                .GetAll(new string[] { "District", "District.Region" })
               .Where(predicate)
               .ProjectTo<SubCountiesModel>(mapper.ConfigurationProvider);

            return await query.ToListAsync(cancellationToken);
        }
        
        public async Task<List<SubCountiesModel>?> GetSubcountiesByFirstThreeCharacters(long countryId, long regionId, long districtId, CancellationToken cancellationToken, String searchText)
        {
            List<SubCountiesModel> unfilteredSubcounties = await  GetSubcounties(countryId, regionId, districtId, cancellationToken);
            if (unfilteredSubcounties != null || unfilteredSubcounties.Count > 0)
            {
                List<SubCountiesModel> filteredList = new List<SubCountiesModel>();
                for (int i = 0; i < unfilteredSubcounties.Count; i++)
                {
                    if (unfilteredSubcounties[i].SubCountyName.ToLower().StartsWith(searchText.Substring(0, 3).ToLower()))
                    {
                        filteredList.Add(unfilteredSubcounties[i]);
                    }
                }
                return filteredList;
            }
            return new List<SubCountiesModel>();
        }

        public async Task<List<ParishModel>?> GetParishes(long countryId, long regionId, long districtId, long subCountyId, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = ParishFilterById(countryId, regionId, districtId, subCountyId);
            Expression<Func<Parish, bool>> predicate = ExpressionBuilder<Parish>.BuildFilterExpression(filters);

            var query = unitOfWork.ParishRepository
                .GetAll(new string[] { "SubCounty", "SubCounty.District", "SubCounty.District.Region" })
               .Where(predicate)
               .ProjectTo<ParishModel>(mapper.ConfigurationProvider);

            return await query.ToListAsync(cancellationToken);
        }
        
        public async Task<List<ParishModel>?> GetParishesByFirstThreeCharacters(long countryId, long regionId, long districtId, long subCountyId, CancellationToken cancellationToken, String searchText)
        {
            List<ParishModel> unfilteredParishes = await GetParishes(countryId, regionId, districtId, subCountyId, cancellationToken);
            if (unfilteredParishes != null || unfilteredParishes.Count > 0)
            {
                List<ParishModel> filteredList = new List<ParishModel>();
                for (int i = 0; i < unfilteredParishes.Count; i++)
                {
                    if (unfilteredParishes[i].ParishName.ToLower().StartsWith(searchText.Substring(0, 3).ToLower()))
                    {
                        filteredList.Add(unfilteredParishes[i]);
                    }
                }
                return filteredList;
            }
            return new List<ParishModel>();
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

        private static Dictionary<string, Models.Common.Filter> SubcountyFilterById(long countryId, long regionId, long districtId)
        {
            var filters = new Dictionary<string, Models.Common.Filter>();

            Filters.AddFilterIfValueGreaterThanZero(filters, countryId, "District.Region.CountryId", SearchOperationEnum.Equal);
            
            Filters.AddFilterIfValueGreaterThanZero(filters, regionId, "District.RegionId", SearchOperationEnum.Equal);
            
            Filters.AddFilterIfValueGreaterThanZero(filters, districtId, "DistrictId", SearchOperationEnum.Equal);

            return filters;
        }

        private static Dictionary<string, Models.Common.Filter> ParishFilterById(long countryId, long regionId, long districtId, long subCountyId)
        {
            var filters = new Dictionary<string, Models.Common.Filter>();

            Filters.AddFilterIfValueGreaterThanZero(filters, countryId, "SubCounty.District.Region.CountryId", SearchOperationEnum.Equal);

            Filters.AddFilterIfValueGreaterThanZero(filters, regionId, "SubCounty.District.RegionId", SearchOperationEnum.Equal);

            Filters.AddFilterIfValueGreaterThanZero(filters, districtId, "SubCounty.DistrictId", SearchOperationEnum.Equal);

            Filters.AddFilterIfValueGreaterThanZero(filters, subCountyId, "SubCountyId", SearchOperationEnum.Equal);

            return filters;
        }

    }
}
