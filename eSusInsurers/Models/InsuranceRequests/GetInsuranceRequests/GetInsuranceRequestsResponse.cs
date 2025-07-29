using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.InsuranceRequests.getInsuranceRequests;

/// <summary>
/// GetInsuranceRequestsResponse
/// </summary>
public class GetInsuranceRequestsResponse
{
    /// <summary>
    /// InsuranceRequests
    /// </summary>
    public PagedResult<InsuranceRequestModel> InsuranceRequests { get; set; } = PagedResult<InsuranceRequestModel>.Empty;
}