using eSusInsurers.Models.enums;

namespace eSusInsurers.Models.Common;

public class Filter
{
    public Filter()
    {
    }

    public Filter(string fieldName, string value, SearchOperationEnum operation, bool useOrLogic = false)
    {
        FieldName = fieldName;
        Value = value;
        Operation = operation;
        UseOrLogic = useOrLogic;
    }

    public string FieldName { get; set; } = string.Empty;
    public string AssocFieldName { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public SearchOperationEnum Operation { get; set; }
    public bool UseOrLogic { get; set; }
}