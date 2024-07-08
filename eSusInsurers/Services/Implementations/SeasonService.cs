using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Constants;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Extensions;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Models.Seasons;
using eSusInsurers.Services.Common;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace eSusInsurers.Services
{
    public class SeasonService(IUnitOfWork unitOfWork, IMapper mapper) : ISeasonService
    {
        public async Task<Models.Common.PagedResult<SeasonModel>> GetSeasons(GetSeasonQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = SeasonsFilters(request);
            Expression<Func<Season, bool>> predicate = ExpressionBuilder<Season>.BuildFilterExpression(filters);
            Dictionary<string, Models.Common.Filter> paginationFilters = FilterHelper.CreatePaginationFilters(request.pagingOptions);
            (int PageSize, int Page) paginationParams = FilterHelper.GetPaginationParams(paginationFilters);
            Dictionary<string, Models.Common.Filter> orderByFilters = FilterHelper.CreateOrderByFilters(request.sortingOptions);
            var orderByParams = OrderByHelper.GetOrderByParams(orderByFilters);

            var query = unitOfWork.SeasonRepository.GetAll()
               .Where(predicate)
               .ProjectTo<SeasonModel>(mapper.ConfigurationProvider);

            if (!string.IsNullOrEmpty(orderByParams) && orderByFilters.ContainsKey(ApplicationConstants.sortBy) && orderByFilters[ApplicationConstants.sortBy].Value.ToLower() == "descending")
            {
                orderByParams += ApplicationConstants.descending;
            }

            if (!string.IsNullOrEmpty(orderByParams))
            {
                query = query.OrderBy(orderByParams);
            }

            var seasons = query.ToPagedResult(paginationParams.Page, paginationParams.PageSize);

            return new Models.Common.PagedResult<SeasonModel>
            {
                PageSize = paginationParams.PageSize,
                TotalPages = seasons.TotalPages,
                TotalRecordCount = seasons.TotalRecordCount,
                Records = seasons.Records,
                CurrentPage = paginationParams.Page
            };

        }

        public async Task<int> AddSeason(SeasonRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var season = await unitOfWork.SeasonRepository.GetBySeasonNameAsync(request.SeasonName, request.SeasonYear, cancellationToken);

            if (season != null)
                throw new BadRequestException($"Season name: ({request.SeasonName}) already exists.");

            season = mapper.Map<Season>(request);

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var entity = await unitOfWork.SeasonRepository.AddAsync(season, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return entity.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }

        public async Task UpdateSeason(int seasonId, UpdateSeasonRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            ArgumentNullException.ThrowIfNull(seasonId, nameof(seasonId));

            var season = await unitOfWork.SeasonRepository.GetByIdAsync(seasonId, null, false, cancellationToken);

            if (season == null)
                throw new NotFoundException("Season Id doesn't exist.");

            var roleName = await unitOfWork.SeasonRepository.GetBySeasonNameAsync(seasonId, request.SeasonName, request.SeasonYear, cancellationToken);

            if (roleName != null)
                throw new BadRequestException($"Season name: ({request.SeasonName}) and Season year: ({request.SeasonYear}) already exists.");

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                mapper.Map(request, season);

                await unitOfWork.SeasonRepository.UpdateAsync(season, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteSeason(int seasonId, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(seasonId, nameof(seasonId));

            var season = await unitOfWork.SeasonRepository.GetByIdAsync(seasonId, null, false, cancellationToken);

            if (season == null)
                throw new NotFoundException($"Season Id: ({seasonId}) doesn't exist.");

            season.IsActive = false;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> ActivateSeason(int seasonId, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(seasonId, nameof(seasonId));

            var season = await unitOfWork.SeasonRepository.GetByIdAsync(seasonId, null, false, cancellationToken);

            if (season == null)
                throw new NotFoundException($"Season Id: ({seasonId}) doesn't exist.");

            season.IsActive = true;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        #region Private Methods
        private static Dictionary<string, Models.Common.Filter> SeasonsFilters(GetSeasonQuery invQuery)
        {
            var inboundDto = invQuery.filter;
            var filters = new Dictionary<string, Models.Common.Filter>();

            if (inboundDto != null)
            {
                Filters.AddFilterIfNotEmpty(filters, inboundDto?.Year, "SeasonYear", SearchOperationEnum.Contains);

                Filters.AddFilterIfNotEmpty(filters, inboundDto?.Season, "SeasonName", SearchOperationEnum.Contains);

                if (inboundDto?.IsActive != null)
                    Filters.AddFilterIfNotEmpty(filters, inboundDto.IsActive == true ? "True" : "False", "IsActive", SearchOperationEnum.Equal);

            }

            return filters;
        }
        #endregion
    }
}
