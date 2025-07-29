using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Constants;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Extensions;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Models.InsuranceRequests.getInsuranceRequests;
using eSusInsurers.Models.Users.GetUsers;
using eSusInsurers.Services.Common;
using eSusInsurers.Services.Interfaces;

namespace eSusInsurers.Services.Implementations
{
    /// <summary>
    /// InsuranceRequestsService
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="configuration"></param>
    /// <param name="mapper"></param>
    /// <param name="tokenService"></param>
    /// <param name="dateTime"></param>
    public class InsuranceRequestsService(IUnitOfWork unitOfWork,
        IMapper mapper): IInsuranceRequestsService
    {
        public async Task<Models.Common.PagedResult<InsuranceRequestModel>> GetInsuranceRequests(GetinsuranceRequestsQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = RequestsFilters(request);
            Expression<Func<InsuranceRequest, bool>> predicate = ExpressionBuilder<InsuranceRequest>.BuildFilterExpression(filters);
            Dictionary<string, Models.Common.Filter> paginationFilters = FilterHelper.CreatePaginationFilters(request.pagingOptions);
            (int PageSize, int Page) paginationParams = FilterHelper.GetPaginationParams(paginationFilters);
            Dictionary<string, Models.Common.Filter> orderByFilters = FilterHelper.CreateOrderByFilters(request.sortingOptions);
            var orderByParams = OrderByHelper.GetOrderByParams(orderByFilters);
            
            
            var query = unitOfWork.InsuranceRequestsRepository.GetAll()
                .Where(predicate)
                .OrderByDescending(x => x.Id)
                .ProjectTo<InsuranceRequestModel>(mapper.ConfigurationProvider);
            if (!string.IsNullOrEmpty(orderByParams) && orderByFilters.ContainsKey(ApplicationConstants.sortBy) && orderByFilters[ApplicationConstants.sortBy].Value.ToLower() == "descending")
            {
                orderByParams += ApplicationConstants.descending;
            }
            
            if (!string.IsNullOrEmpty(orderByParams))
            {
                query = query.OrderBy(orderByParams);
            }
            var insuranceRequests = query.ToPagedResult(paginationParams.Page, paginationParams.PageSize);
            return new Models.Common.PagedResult<InsuranceRequestModel>
            {
                PageSize = paginationParams.PageSize,
                TotalPages = insuranceRequests.TotalPages,
                TotalRecordCount = insuranceRequests.TotalRecordCount,
                Records = insuranceRequests.Records,
                CurrentPage = paginationParams.Page
            };
        }
        
        
        
        #region Private Methods
        private static Dictionary<string, Models.Common.Filter> RequestsFilters(GetinsuranceRequestsQuery invQuery)
        {
            var inboundDto = invQuery.filter;
            var filters = new Dictionary<string, Models.Common.Filter>();

            if (inboundDto != null)
            {
                if (inboundDto?.IsActive != null)
                    Filters.AddFilterIfNotEmpty(filters, inboundDto.IsActive == true ? "True" : "False", "IsActive", SearchOperationEnum.Equal);
            }

            return filters;
        }
        #endregion
    } 
}

