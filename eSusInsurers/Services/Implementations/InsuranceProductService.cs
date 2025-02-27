using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Constants;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Extensions;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Models.InsuranceProducts;
using eSusInsurers.Services.Common;
using eSusInsurers.Services.Interfaces;

namespace eSusInsurers.Services.Implementations;

public class InsuranceProductService(
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IInsuranceProductService
{
    public async Task<Models.Common.PagedResult<InsuranceProductModel>> GetInsuranceProducts(
        InsuranceProductQuery request, CancellationToken cancellationToken)
    {
        Dictionary<string, Models.Common.Filter> filters = InsuranceProductFilters(request);
        Expression<Func<InsurancePolicy1, bool>> predicate =
            ExpressionBuilder<InsurancePolicy1>.BuildFilterExpression(filters);
        Dictionary<string, Models.Common.Filter> paginationFilters =
            FilterHelper.CreatePaginationFilters(request.pagingOptions);
        var paginationParams = FilterHelper.GetPaginationParams(paginationFilters);
        Dictionary<string, Models.Common.Filter> orderByFilters =
            FilterHelper.CreateOrderByFilters(request.sortingOptions);
        var orderByParams = OrderByHelper.GetOrderByParams(orderByFilters);

        var query = unitOfWork.InsuranceProductRepository.GetAll(
                new[]
                {
                    "Category", "Company"
                })
            .Where(predicate)
            .OrderByDescending(x => x.Id)
            .ProjectTo<InsuranceProductModel>(mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(orderByParams) && orderByFilters.ContainsKey(ApplicationConstants.sortBy) &&
            orderByFilters[ApplicationConstants.sortBy].Value.ToLower() == "descending")
            orderByParams += ApplicationConstants.descending;

        if (!string.IsNullOrEmpty(orderByParams)) query = query.OrderBy(orderByParams);

        var insuranceProduct = query.ToPagedResult(paginationParams.Page, paginationParams.PageSize);

        return new Models.Common.PagedResult<InsuranceProductModel>
        {
            PageSize = paginationParams.PageSize,
            TotalPages = insuranceProduct.TotalPages,
            TotalRecordCount = insuranceProduct.TotalRecordCount,
            Records = insuranceProduct.Records,
            CurrentPage = paginationParams.Page
        };
    }


    private static Dictionary<string, Models.Common.Filter> InsuranceProductFilters(InsuranceProductQuery invQuery)
    {
        var inboundDto = invQuery.filter;
        var filters = new Dictionary<string, Models.Common.Filter>();

        if (inboundDto != null)
            if (inboundDto?.IsActive != null)
                Filters.AddFilterIfNotEmpty(filters, inboundDto.IsActive == true ? "True" : "False", "IsActive",
                    SearchOperationEnum.Equal);

        return filters;
    }
}