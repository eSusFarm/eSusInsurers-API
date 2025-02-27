using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.InsuranceProducts;

public class InsuranceProductQuery
{
    public PagingOptions pagingOptions { get; set; }
    public InsuranceProductFilterOption? filter { get; set; }
    public SortingOptions? sortingOptions { get; set; }
}