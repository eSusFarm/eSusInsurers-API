using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Constants;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.Extensions;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Models.SeasonCutOffDate;
using eSusInsurers.Services.Common;
using eSusInsurers.Services.Interfaces;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using eSusInsurers.Models.enums;
using eSusInsurers.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Services.Implementations
{
    public class SeasonCutOffDateService(IUnitOfWork unitOfWork, IMapper mapper) : ISeasonCutOffDateService
    {
        public async Task<SeasonCutOffDatesModel?> GetSeasonCutOffDatesById(int seasonCutOffDateId, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = SeasonCutOffDatesFiltersById(seasonCutOffDateId);
            Expression<Func<SeasonCutOffDate, bool>> predicate = ExpressionBuilder<SeasonCutOffDate>.BuildFilterExpression(filters);

            var query = unitOfWork.SeasonCutOffDateRepository.GetAll()
               .Where(predicate)
               .ProjectTo<SeasonCutOffDatesModel>(mapper.ConfigurationProvider);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Models.Common.PagedResult<SeasonCutOffDatesModel>> GetSeasonCutOffDates(GetSeasonCutOffDatesQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = SeasonCutOffDatesFilters(request);
            Expression<Func<SeasonCutOffDate, bool>> predicate = ExpressionBuilder<SeasonCutOffDate>.BuildFilterExpression(filters);
            Dictionary<string, Models.Common.Filter> paginationFilters = FilterHelper.CreatePaginationFilters(request.pagingOptions);
            (int PageSize, int Page) paginationParams = FilterHelper.GetPaginationParams(paginationFilters);
            Dictionary<string, Models.Common.Filter> orderByFilters = FilterHelper.CreateOrderByFilters(request.sortingOptions);
            var orderByParams = OrderByHelper.GetOrderByParams(orderByFilters);

            var query = unitOfWork.SeasonCutOffDateRepository.GetAll(new string[]
                   {
                       "Crop",  "CropCategory",  "Region",  "Season"
                   })
               .Where(predicate)
               .ProjectTo<SeasonCutOffDatesModel>(mapper.ConfigurationProvider);

            if (!string.IsNullOrEmpty(orderByParams) && orderByFilters.ContainsKey(ApplicationConstants.sortBy) && orderByFilters[ApplicationConstants.sortBy].Value.ToLower() == "descending")
            {
                orderByParams += ApplicationConstants.descending;
            }

            if (!string.IsNullOrEmpty(orderByParams))
            {
                query = query.OrderBy(orderByParams);
            }

            var seasons = query.ToPagedResult(paginationParams.Page, paginationParams.PageSize);

            return new Models.Common.PagedResult<SeasonCutOffDatesModel>
            {
                PageSize = paginationParams.PageSize,
                TotalPages = seasons.TotalPages,
                TotalRecordCount = seasons.TotalRecordCount,
                Records = seasons.Records,
                CurrentPage = paginationParams.Page
            };

        }

        public async Task<bool> AddSeasonCutOffDates(List<SeasonCutOffDatesRequest> request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            foreach (var season in request)
            {
                var seasonCutOffDates = await unitOfWork.SeasonCutOffDateRepository.GetBySeasonCutOffDateAsync(season.SeasonId, season.RegionId, season.CropCategoryId, season.CropId, cancellationToken);

                if (seasonCutOffDates != null)
                    throw new BadRequestException($"Request already exists.");
            }

            var seasonCutOffDate = mapper.Map<List<SeasonCutOffDate>>(request);

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await unitOfWork.SeasonCutOffDateRepository.AddRangeAsync(seasonCutOffDate, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }

        public async Task<bool> SeasonCutOffDatesExistenceCheck(SeasonCutOffDatesRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            var seasonCutOffDates = await unitOfWork.SeasonCutOffDateRepository.GetBySeasonCutOffDateAsync(request.SeasonId, request.RegionId, request.CropCategoryId, request.CropId, cancellationToken);

            if (seasonCutOffDates != null)
                throw new BadRequestException($"Request already exists.");
            return true;

        }
        public async Task UpdateSeasonCutOffDates(int seasonCutOffDateId, UpdateSeasonCutOffDatesRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            ArgumentNullException.ThrowIfNull(seasonCutOffDateId, nameof(seasonCutOffDateId));
            var seasonCutOffDates = await unitOfWork.SeasonCutOffDateRepository.GetByIdAsync(seasonCutOffDateId, null, false, cancellationToken);
            if (seasonCutOffDates == null)
                throw new NotFoundException("Season CutOff Date Id doesn't exist.");
            var seasonCutOffDate = await unitOfWork.SeasonCutOffDateRepository.GetBySeasonCutOffDateAsync(seasonCutOffDateId, request.SeasonId, request.RegionId, request.CropCategoryId, request.CropId, cancellationToken);


            if (seasonCutOffDate != null)
                throw new BadRequestException($"Request already exists.");

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                mapper.Map(request, seasonCutOffDates);

                await unitOfWork.SeasonCutOffDateRepository.UpdateAsync(seasonCutOffDates, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteSeasonCutOffDates(int seasonCutOffDateId, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(seasonCutOffDateId, nameof(seasonCutOffDateId));

            var seasonCutOffDates = await unitOfWork.SeasonRepository.GetByIdAsync(seasonCutOffDateId, null, false, cancellationToken);

            if (seasonCutOffDates == null)
                throw new NotFoundException($"Season CutOff Date Id: ({seasonCutOffDateId}) doesn't exist.");

            seasonCutOffDates.IsActive = false;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> ActivateSeasonCutOffDates(int seasonCutOffDateId, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(seasonCutOffDateId, nameof(seasonCutOffDateId));

            var seasonCutOffDates = await unitOfWork.SeasonRepository.GetByIdAsync(seasonCutOffDateId, null, false, cancellationToken);

            if (seasonCutOffDates == null)
                throw new NotFoundException($"Season CutOff Date Id: ({seasonCutOffDateId}) doesn't exist.");

            seasonCutOffDates.IsActive = true;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        #region Private Methods
        private static Dictionary<string, Models.Common.Filter> SeasonCutOffDatesFilters(GetSeasonCutOffDatesQuery invQuery)
        {
            var inboundDto = invQuery.filter;
            var filters = new Dictionary<string, Models.Common.Filter>();

            if (inboundDto != null)
            {
                Filters.AddFilterIfNotEmpty(filters, (inboundDto?.Region != null && inboundDto.Region.Count() > 0) ? string.Join(",", inboundDto.Region) : "", "Region.RegionName", SearchOperationEnum.LContains);

                Filters.AddFilterIfNotEmpty(filters, (inboundDto?.Crop != null && inboundDto.Crop.Count() > 0) ? string.Join(",", inboundDto.Crop) : "", "Crop.CropName", SearchOperationEnum.LContains);

                Filters.AddFilterIfNotEmpty(filters, (inboundDto?.CropCategory != null && inboundDto.CropCategory.Count() > 0) ? string.Join(",", inboundDto.CropCategory) : "", "CropCategory.CropCategoryName", SearchOperationEnum.LContains);

                Filters.AddFilterIfNotEmpty(filters, (inboundDto?.Year != null && inboundDto.Year.Count() > 0) ? string.Join(",", inboundDto.Year) : "", "Season.SeasonYear", SearchOperationEnum.LContains);

                Filters.AddFilterIfNotEmpty(filters, (inboundDto?.Season != null && inboundDto.Season.Count() > 0) ? string.Join(",", inboundDto.Season) : "", "Season.SeasonName", SearchOperationEnum.LContains);

                if (inboundDto?.IsActive != null)
                    Filters.AddFilterIfNotEmpty(filters, inboundDto.IsActive == true ? "True" : "False", "IsActive", SearchOperationEnum.Equal);

            }

            return filters;
        }

        private static Dictionary<string, Models.Common.Filter> SeasonCutOffDatesFiltersById(int seasonCutOffDateId)
        {
            var filters = new Dictionary<string, Models.Common.Filter>();

            Filters.AddFilterIfValueGreaterThanZero(filters, seasonCutOffDateId, "Id", SearchOperationEnum.Equal);

            return filters;
        }
        #endregion
    }
}
