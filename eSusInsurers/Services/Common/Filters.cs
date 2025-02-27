using eSusInsurers.Models.enums;

namespace eSusInsurers.Services.Common;

public class Filters
{
    public static void AddFilterIfNotEmpty(Dictionary<string, Models.Common.Filter> filters, string? value,
        string fieldName, SearchOperationEnum operation, bool useOr = false)
    {
        if (!string.IsNullOrEmpty(value)) filters.Add(fieldName, CreateFilter(fieldName, value, operation, useOr));
    }

    public static void AddFilterIfValueGreaterThanZero(Dictionary<string, Models.Common.Filter> filters, long? value,
        string fieldName, SearchOperationEnum operation, bool useOr = false)
    {
        if (value.HasValue && value.Value > 0)
            filters.Add(fieldName, CreateFilter(fieldName, value.Value.ToString(), operation, useOr));
    }

    public static Models.Common.Filter CreateFilter(string fieldName, string value, SearchOperationEnum operation,
        bool useOrLogic = false)
    {
        return new Models.Common.Filter
        {
            FieldName = fieldName,
            Value = value,
            Operation = operation,
            UseOrLogic = useOrLogic
        };
    }

    internal static void AddFilterIfNotEmpty(Dictionary<string, Models.Common.Filter> filters, int cropCategoryId,
        string v, SearchOperationEnum equal)
    {
        throw new NotImplementedException();
    }
}