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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("insuranceProduct")]
    public class InsuranceProductController(IInsuranceProductService insuranceProduct) : BaseController
    {
        /// <summary>
        /// Get Insurance Product
        /// </summary>
        /// <remarks>
        /// Returns a paginated list of insurance product.
        /// </remarks>
        /// <param name="pagingOptions">Pagination options for response.</param>
        ///   /// <param name="filter">Data filter options.</param>
        /// <param name="sort">Data sorting options.</param>
        /// <response code="200">Returns a paginated list of insurance product.</response>
        /// <returns>Paginated list of insurance product.</returns>
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InsuranceProductResponse))]
        public async Task<ActionResult<InsuranceProductResponse>> GetInsuranceProduct([FromQuery] PagingOptions pagingOptions = default!,
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

                var result = await insuranceProduct.GetInsuranceProduct(query, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}
