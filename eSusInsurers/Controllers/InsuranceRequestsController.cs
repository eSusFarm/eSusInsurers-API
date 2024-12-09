using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceRequests.getInsuranceRequests;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for Insurance Requests
    /// </summary>
    [Route("insurance-requests")]
    public class InsuranceRequestsController(IInsuranceRequestsService insuranceRequestsService): BaseController
    {
        /// <summary>
        /// Get Insurance Requests
        /// </summary>
        /// <remarks>
        /// Returns a paginated list of insurance requests.
        /// </remarks>
        /// <param name="pagingOptions">Pagination options for response.</param>
        /// <param name="filter">Data filter options.</param>
        /// <param name="sort">Data sorting options.</param>
        /// <response code="200">Returns a paginated list of users.</response>
        /// <returns>Paginated list of users.</returns>
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetInsuranceRequestsResponse))]
        public async Task<ActionResult<GetInsuranceRequestsResponse>> GetUsers([FromQuery] PagingOptions pagingOptions = default!,
            [FromQuery]  InsuranceRequestFilterOptions filter= default!,
            [FromQuery] SortingOptions sort = default!)
        {
            try
            {

                var query = new GetinsuranceRequestsQuery()
                {
                    pagingOptions = pagingOptions,
                    filter = filter,
                    sortingOptions = sort
                };

                var result = await insuranceRequestsService.GetInsuranceRequests(query, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}

