using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.InsuranceProducts;

public class InsuranceProductResponse
{
    public PagedResult<InsuranceProductModel> InsuranceProduct { get; set; } = PagedResult<InsuranceProductModel>.Empty;
}