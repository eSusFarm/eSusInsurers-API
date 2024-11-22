using eSusInsurers.Models.Common;
using eSusInsurers.Models.enums;

namespace eSusInsurers.Models.Helpers;

public static class FilterHelper
{
    public static Dictionary<string, Common.Filter> CreatePaginationFilters(PagingOptions pageInfo)
    {
        var paginationFilters = new Dictionary<string, Common.Filter>();

        if (pageInfo != null)
        {
            paginationFilters.Add(nameof(pageInfo.Page),
                CreateFilter("Page", pageInfo.Page?.ToString() ?? "", SearchOperationEnum.Equal));
            paginationFilters.Add(nameof(pageInfo.PageSize),
                CreateFilter("PageSize", pageInfo.PageSize?.ToString() ?? "", SearchOperationEnum.Equal));
        }

        return paginationFilters;
    }

    public static Dictionary<string, Common.Filter> CreateOrderByFilters(SortingOptions sortInfo)
    {
        var orderByFilters = new Dictionary<string, Common.Filter>();

        if (sortInfo != null)
        {
            AddFilterIfNotEmpty(orderByFilters, sortInfo.SortDirection, "SortBy", SearchOperationEnum.Equal);
            AddFilterIfNotEmpty(orderByFilters, sortInfo.SortOn, "OrderBy", SearchOperationEnum.Equal);
        }

        return orderByFilters;
    }

    private static Common.Filter CreateFilter(string fieldName, string value, SearchOperationEnum operation)
    {
        return new Common.Filter
        {
            FieldName = fieldName,
            Value = value,
            Operation = operation
        };
    }

    public static (int PageSize, int Page) GetPaginationParams(Dictionary<string, Common.Filter> filters)
    {
        int page = -1, pageSize = -1;

        var key = "Page";
        if (filters.ContainsKey(key)) page = int.Parse(filters[key].Value);

        key = "PageSize";
        if (filters.ContainsKey(key)) pageSize = int.Parse(filters[key].Value);

        return (pageSize, page);
    }

    public static void AddFilterIfNotEmpty(Dictionary<string, Common.Filter> filters, string value, string fieldName,
        SearchOperationEnum operation)
    {
        if (!string.IsNullOrEmpty(value)) filters.Add(fieldName, CreateFilter(fieldName, value, operation));
    }

    public static void AddFilterIfValueGreaterThanZero(Dictionary<string, Common.Filter> filters, int? value,
        string fieldName, SearchOperationEnum operation)
    {
        if (value.HasValue && value.Value > 0)
            filters.Add(fieldName, CreateFilter(fieldName, value.Value.ToString(), operation));
    }
}