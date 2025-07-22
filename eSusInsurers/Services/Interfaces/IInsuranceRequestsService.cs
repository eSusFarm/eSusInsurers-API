using eSusInsurers.Models.InsuranceRequests.getInsuranceRequests;

namespace eSusInsurers.Services.Interfaces
{
    /// <summary>
    /// Service that handles the insurance requests data.
    /// </summary>
    public interface IInsuranceRequestsService
    {
        /// <summary>
        /// GetInsuranceRequests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Models.Common.PagedResult<InsuranceRequestModel>>GetInsuranceRequests(GetinsuranceRequestsQuery request, CancellationToken cancellationToken);
    }
}