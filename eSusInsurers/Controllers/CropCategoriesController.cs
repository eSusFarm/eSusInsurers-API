using eSusInsurers.Models;
using eSusInsurers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers;

/// <summary>
///     Controller for cropcategories.
/// </summary>
[Route("cropcategories")]
public class CropCategoriesController(ICropCategoryService cropCategoryService) : ControllerBase
{
    /// <summary>
    ///     Get crop categories
    /// </summary>
    /// <remarks>
    ///     Returns crop categories.
    /// </remarks>
    /// <response code="200">Returns crop categories.</response>
    /// <returns>Returns crop categories</returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CropCategoryModel>))]
    public async Task<ActionResult<List<CropCategoryModel>>> GetCropCategories()
    {
        try
        {
            var result = await cropCategoryService.GetCropCategories(new CancellationToken());

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }
}