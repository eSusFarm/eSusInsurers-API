using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceProducts;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for managing insurance products.
    /// </summary>
    [Route("insuranceProduct")]
    public class InsuranceProductController(IInsuranceProductService insuranceProductService) : BaseController
    {
        /// <summary>
        /// Get Insurance Products
        /// </summary>
        /// <remarks>
        /// Returns a paginated list of insurance products.
        /// </remarks>
        /// <param name="pagingOptions">Pagination options for response.</param>
        /// <param name="filter">Data filter options.</param>
        /// <param name="sort">Data sorting options.</param>
        /// <response code="200">Returns a paginated list of insurance products.</response>
        /// <returns>Paginated list of insurance products.</returns>
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InsuranceProductResponse))]
        public async Task<ActionResult<InsuranceProductResponse>> GetInsuranceProducts([FromQuery] PagingOptions pagingOptions = default!,
              [FromQuery] InsuranceProductFilterOption filter = default!,
           [FromQuery] SortingOptions sort = default!)
        {
            try
            {

                var query = new InsuranceProductQuery
                {
                    pagingOptions = pagingOptions,
                    filter = filter,
                    sortingOptions = sort
                };

                var result = await insuranceProductService.GetInsuranceProducts(query, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}
