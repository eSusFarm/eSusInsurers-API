using eSusInsurers.Models.Countries;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for countries.
    /// </summary>
    [Route("countries")]
    public class CountriesController(ICountriesService countriesService) : BaseController
    {
        /// <summary>
        /// Get Regions By Country_id
        /// </summary>
        /// <remarks>
        /// Returns Regions.
        /// </remarks>
        /// <param name="country_id">country id.</param>
        /// <response code="200">Returns regions.</response>
        /// <returns>Returns regions</returns>
        [HttpGet("{country_id}/regions"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RegionModel>))]
        public async Task<ActionResult<List<RegionModel>>> GetRegions(long country_id)
        {
            try
            {
                var result = await countriesService.GetRegions(country_id, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Get Districts By region_id
        /// </summary>
        /// <remarks>
        /// Returns Districts.
        /// </remarks>
        /// <param name="region_id">Region_id</param>
        /// <response code="200">Returns districts.</response>
        /// <returns>Returns districts</returns>
        [HttpGet("{region_id}/districts"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DistrictModel>))]
        public async Task<ActionResult<List<DistrictModel>>> GetDistricts(long region_id)
        {
            try
            {
                var result = await countriesService.GetDistricts(region_id, new CancellationToken());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Get subcounties By district_id
        /// </summary>
        /// <remarks>
        /// Returns subcounties.
        /// </remarks>
        /// <param name="district_id">district_id</param>
        /// <response code="200">Returns subcounties.</response>
        /// <returns>Returns subcounties</returns>
        [HttpGet("{district_id}/subcounties"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DistrictModel>))]
        public async Task<ActionResult<List<SubCountiesModel>>> GetSubcounties(long district_id)
        {
            try
            {
                var result = await countriesService.GetSubcounties(district_id, new CancellationToken());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}
