using eSusInsurers.Models.Common;
using eSusInsurers.Models.Seasons;
using eSusInsurers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for seasons.
    /// </summary>
    [Authorize]
    [Route("seasons")]
    public class SeasonsController(ISeasonService seasonsService) : ControllerBase
    {
        /// <summary>
        /// Get seasons
        /// </summary>
        /// <remarks>
        /// Returns a paginated list of seasons.
        /// </remarks>
        /// <param name="pagingOptions">Pagination options for response.</param>
        /// <param name="filter">Data filter options.</param>
        /// <param name="sort">Data sorting options.</param>
        /// <response code="200">Returns a paginated list of seasons.</response>
        /// <returns>Paginated list of seasons.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSeasonsResponse))]
        public async Task<ActionResult<GetSeasonsResponse>> GetSeasons([FromQuery] PagingOptions pagingOptions = default!,
           [FromQuery] SeasonFilterOptions filter = default!,
           [FromQuery] SortingOptions sort = default!)
        {
            try
            {
                var query = new GetSeasonQuery
                {
                    pagingOptions = pagingOptions,
                    filter = filter,
                    sortingOptions = sort
                };

                var result = await seasonsService.GetSeasons(query, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Get seasons by year
        /// </summary>
        /// <remarks>
        /// Returns a list of seasons by year.
        /// </remarks>
        /// <param name="year"></param>
        /// <response code="200">Returns a list of seasons by year.</response>
        [HttpGet("year")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SeasonModel))]
        public async Task<ActionResult<SeasonModel>> GetSeasonByYear(string year)
        {
            try
            {
                var result = await seasonsService.GetSeasonByYear(year, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }   

        /// <summary>
        /// Add new season
        /// </summary>
        /// <remarks>
        /// Add new season
        /// </remarks>
        /// <param name="request">Information of the season to add</param>
        /// <response code="201">Indicates the season is successfully created.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddSeason([FromBody] SeasonRequest request)
        {
            try
            {
                var response = await seasonsService.AddSeason(request, new CancellationToken());

                return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }

        /// <summary>
        /// Update a season
        /// </summary>
        /// <remarks>
        /// Update a season
        /// </remarks>
        /// <param name="request">season details of the season</param>
        /// <param name="seasonId">season id of the seasons</param>
        /// <response code="204">Indicates the season details is updated</response>
        [HttpPut("{seasonId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSeason(int seasonId, [FromBody] UpdateSeasonRequest request)
        {
            try
            {
                await seasonsService.UpdateSeason(seasonId, request, new CancellationToken());

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Deactivate a season
        /// </summary>
        /// <remarks>
        /// Deactivate a season
        /// </remarks>
        /// <param name="seasonId">Id of the season</param>
        /// <response code="204">Indicates the season is inactive</response>
        [HttpDelete("{seasonId}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSeason(int seasonId)
        {
            try
            {
                var response = await seasonsService.DeleteSeason(seasonId, new CancellationToken());
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Activate a season
        /// </summary>
        /// <remarks>
        /// Activate a season
        /// </remarks>
        /// <param name="seasonId">Id of the season</param>
        /// <response code="204">Indicates the season is active</response>
        [HttpPut("{seasonId}/activate"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ActivateSeason(int seasonId)
        {
            try
            {
                var response = await seasonsService.ActivateSeason(seasonId, new CancellationToken());
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}
