using eSusInsurers.Models.Countries;
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
        /// Get regions by country Id
        /// </summary>
        /// <remarks>
        /// Returns regions.
        /// </remarks>
        /// <param name="countryId">Country Id.</param>
        /// <response code="200">Returns regions.</response>
        /// <returns>Returns regions</returns>
        [HttpGet("{countryId}/regions"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RegionModel>))]
        public async Task<ActionResult<List<RegionModel>>> GetRegions(long countryId)
        {
            try
            {
                var result = await countriesService.GetRegions(countryId, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Get districts by region id
        /// </summary>
        /// <remarks>
        /// Returns districts.
        /// </remarks>
        /// <param name="regionId">Region Id</param>
        /// <param name="countryId">Country Id</param>
        /// <response code="200">Returns districts.</response>
        /// <returns>Returns districts</returns>
        [HttpGet("{countryId}/regions/{regionId}/districts"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DistrictModel>))]
        public async Task<ActionResult<List<DistrictModel>>> GetDistricts(long countryId, long regionId)
        {
            try
            {
                var result = await countriesService.GetDistricts(countryId, regionId, new CancellationToken());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Get subcounties by district id
        /// </summary>
        /// <remarks>
        /// Returns subcounties.
        /// </remarks>
        /// <param name="regionId">Region Id</param>
        /// <param name="countryId">Country Id</param>
        /// <param name="districtId">District Id</param>
        /// <response code="200">Returns subcounties.</response>
        /// <returns>Returns subcounties</returns>
        [HttpGet("{countryId}/regions/{regionId}/districts{districtId}/subcounties"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DistrictModel>))]
        public async Task<ActionResult<List<SubCountiesModel>>> GetSubcounties(long countryId, long regionId, long districtId)
        {
            try
            {
                var result = await countriesService.GetSubcounties(countryId, regionId, districtId, new CancellationToken());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}
