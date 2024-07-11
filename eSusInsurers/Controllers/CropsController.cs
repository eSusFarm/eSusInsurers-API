using eSusInsurers.Models;
using eSusInsurers.Models.Seasons;
using eSusInsurers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for crops.
    /// </summary>
    [Route("crops")]
    public class CropsController(ICropService cropService) : ControllerBase
    {
        /// <summary>
        /// Get crops by cropCategoryId
        /// </summary>
        /// <remarks>
        /// Returns a list of crops by cropCategoryId
        /// </remarks>
        /// <param name="cropCategoryId"></param>
        /// <response code="200">Returns a list of crops by cropCategoryId.</response>
        [HttpGet("{cropCategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CropModel))]
        public async Task<ActionResult<CropModel>> GetCropsByCropCategoryId(int cropCategoryId)
        {
            try
            {
                var result = await cropService.GetCropsByCropCategoryId(cropCategoryId, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}
