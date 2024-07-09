using eSusInsurers.Models.Common;
using eSusInsurers.Models.Programs;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for prograns.
    /// </summary>
    [Route("programs")]
    public class ProgramsController(IProgramsService programsService) : BaseController
    {
        /// <summary>
        /// Get programs
        /// </summary>
        /// <remarks>
        /// Returns a paginated list of programs.
        /// </remarks>
        /// <param name="pagingOptions">Pagination options for response.</param>
        /// <param name="filter">Data filter options.</param>
        /// <param name="sort">Data sorting options.</param>
        /// <response code="200">Returns a paginated list of programs.</response>
        /// <returns>Paginated list of programs.</returns>
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProgramResponse))]
        public async Task<ActionResult<GetProgramResponse>> Getprograms([FromQuery] PagingOptions pagingOptions = default!,
           [FromQuery] ProgramFilterOptions filter = default!,
           [FromQuery] SortingOptions sort = default!)
        {
            try
            {

                var query = new GetProgramsQuery
                {
                    pagingOptions = pagingOptions,
                    filter = filter,
                    sortingOptions = sort
                };

                var result = await programsService.GetPrograms(query, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Add new program
        /// </summary>
        /// <remarks>
        /// Add new program
        /// </remarks>
        /// <param name="request">Information of the program to register</param>
        /// <response code="201">Indicates the program is successfully created.</response>
        [HttpPost, Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddProgram([FromBody] ProgramRequest request)
        {
            try
            {
                var response = await programsService.AddProgram(request, new CancellationToken());

                return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }


        /// <summary>
        /// Update a program
        /// </summary>
        /// <remarks>
        /// Update a program
        /// </remarks>
        /// <param name="request">Program details of the program</param>
        /// <param name="programId">Program id of the program</param>
        /// <response code="204">Indicates the program details is updated</response>
        [HttpPut("{programId}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProgram(int programId, ProgramRequest request)
        {
            try
            {
                await programsService.UpdateProgram(programId, request, new CancellationToken());

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Deactivate a program
        /// </summary>
        /// <remarks>
        /// Deactivate a program
        /// </remarks>
        /// <param name="programId">Program id of the program</param>
        /// <response code="204">Indicates the program is inactive</response>
        [HttpDelete("{programId}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProgram(int programId)
        {
            try
            {
                await programsService.DeleteProgram(programId, new CancellationToken());
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }


        /// <summary>
        /// Activate a program
        /// </summary>
        /// <remarks>
        /// Activate a program
        /// </remarks>
        /// <param name="programId">Program Id of the program</param>
        /// <response code="204">Indicates the program is active</response>
        [HttpPut("{programId}/activate"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ActivateProgram(int programId)
        {
            try
            {
                await programsService.ActivateProgram(programId, new CancellationToken());
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}
