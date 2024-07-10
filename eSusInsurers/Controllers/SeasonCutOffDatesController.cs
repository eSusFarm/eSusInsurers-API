using eSusInsurers.Domain.Entities;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.SeasonCutOffDate;
using eSusInsurers.Models.Seasons;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for seasons.
    /// </summary>
    [Authorize]
    [Route("seasonscutoffdates")]
    public class SeasonCutOffDatesController(ISeasonCutOffDateService seasonCutOffDateService) : ControllerBase
    {

        /// <summary>
        /// Get season cutoff dates
        /// </summary>
        /// <remarks>
        /// Returns a paginated list of season cutoff dates.
        /// </remarks>
        /// <param name="pagingOptions">Pagination options for response.</param>
        /// <param name="filter">Data filter options.</param>
        /// <param name="sort">Data sorting options.</param>
        /// <response code="200">Returns a paginated list of seasons.</response>
        /// <returns>Paginated list of season cutoff dates.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSeasonCutOffDatesResponse))]
        public async Task<ActionResult<GetSeasonCutOffDatesResponse>> GetSeasonCutOffDates([FromQuery] PagingOptions pagingOptions = default!,
           [FromQuery] SeasonCutOffDatesFilterOptions filter = default!,
           [FromQuery] SortingOptions sort = default!)
        {
            try
            {
                var query = new GetSeasonCutOffDatesQuery
                {
                    pagingOptions = pagingOptions,
                    filter = filter,
                    sortingOptions = sort
                };

                var result = await seasonCutOffDateService.GetSeasonCutOffDates(query, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Get seasons cutoff dates by seasonCutOffDateId
        /// </summary>
        /// <remarks>
        /// Returns a list of seasons cutoff dates by seasonCutOffDateId.
        /// </remarks>
        /// <param name="seasonCutOffDateId"></param>
        /// <response code="200">Returns a list of seasons cutoff dates by seasonCutOffDateId.</response>
        [HttpGet("{seasonCutOffDateId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SeasonCutOffDatesModel))]
        public async Task<ActionResult<SeasonCutOffDatesModel>> GetSeasonById(int seasonCutOffDateId)
        {
            try
            {
                var result = await seasonCutOffDateService.GetSeasonCutOffDatesById(seasonCutOffDateId, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Add new season cutoff dates
        /// </summary>
        /// <remarks>
        /// Add new season cutoff dates
        /// </remarks>
        /// <param name="request">Information of the season cutoff dates to add</param>
        /// <response code="201">Indicates the season cutoff dates is successfully created.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddSeasonCutOffDates([FromBody] List<SeasonCutOffDatesRequest> request)
        {
            try
            {
                var response = await seasonCutOffDateService.AddSeasonCutOffDates(request, new CancellationToken());

                return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }

        /// <summary>
        /// Update a season cutoff dates
        /// </summary>
        /// <remarks>
        /// Update a season cutoff dates
        /// </remarks>
        /// <param name="request">season details of the season cutoff dates</param>
        /// <param name="seasonCutOffDateId">season id of the seasons cutoff dates</param>
        /// <response code="204">Indicates the season cutoff dates details is updated</response>
        [HttpPut("{seasonCutOffDateId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSeason(int seasonCutOffDateId, [FromBody] UpdateSeasonCutOffDatesRequest request)
        {
            try
            {
                await seasonCutOffDateService.UpdateSeasonCutOffDates(seasonCutOffDateId, request, new CancellationToken());

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Deactivate a season cutoff dates
        /// </summary>
        /// <remarks>
        /// Deactivate a season cutoff dates
        /// </remarks>
        /// <param name="seasonCutOffDateId">Id of the season cutoff dates</param>
        /// <response code="204">Indicates the season cutoff dates is inactive</response>
        [HttpDelete("{seasonCutOffDateId}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSeason(int seasonCutOffDateId)
        {
            try
            {
                var response = await seasonCutOffDateService.DeleteSeasonCutOffDates(seasonCutOffDateId, new CancellationToken());
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Activate a season cutoff dates
        /// </summary>
        /// <remarks>
        /// Activate a season cutoff dates
        /// </remarks>
        /// <param name="seasonCutOffDateId">Id of the season cutoff dates</param>
        /// <response code="204">Indicates the season cutoff dates is active</response>
        [HttpPut("{seasonCutOffDateId}/activate"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ActivateSeason(int seasonCutOffDateId)
        {
            try
            {
                var response = await seasonCutOffDateService.ActivateSeasonCutOffDates(seasonCutOffDateId, new CancellationToken());
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Add new season cutoff dates
        /// </summary>
        /// <remarks>
        /// Add new season cutoff dates
        /// </remarks>
        /// <param name="request">Information of the season cutoff dates to add</param>
        /// <response code="201">Indicates the season cutoff dates is successfully created.</response>
        [HttpPost("existence_check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSeasonCutOffDates([FromBody] SeasonCutOffDatesRequest request)
        {
            try
            {
                var response = await seasonCutOffDateService.SeasonCutOffDatesExistenceCheck(request, new CancellationToken());

                return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }
    }
}
