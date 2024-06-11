using eSusInsurers.Models.Common;
using eSusInsurers.Models.Roles.GetRoles;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Models.Roles.UpdateRole;
using WMS.Models.Roles;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for roles.
    /// </summary>
    //[Authorize]
    [Route("roles")]
    public class RoleController(IRolesService rolesService) : BaseController
    {
        /// <summary>
        /// To get ApplicationMenus, SubMenus and functionalities
        /// </summary>
        /// <returns>
        /// To get ApplicationMenus, SubMenus and functionalities
        /// </returns>
        [HttpGet("application_menus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationMenuItems))]
        public async Task<ActionResult<ApplicationMenuItems>> GetApplicationMenuItemsMasterList(CancellationToken cancellationToken)
        {
            try
            {
                var result = await rolesService.GetApplicationMenuItems(cancellationToken);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Get roles
        /// </summary>
        /// <remarks>
        /// Returns a paginated list of roles.
        /// </remarks>
        /// <param name="pagingOptions">Pagination options for response.</param>
        /// <param name="filter">Data filter options.</param>
        /// <param name="sort">Data sorting options.</param>
        /// <response code="200">Returns a paginated list of roles.</response>
        /// <returns>Paginated list of roles.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRolesResponse))]
        public async Task<ActionResult<GetRolesResponse>> GetRoles([FromQuery] PagingOptions pagingOptions = default!,
           [FromQuery] RoleFilterOptions filter = default!,
           [FromQuery] SortingOptions sort = default!)
        {
            try
            {

                var query = new GetRolesQuery
                {
                    pagingOptions = pagingOptions,
                    filter = filter,
                    sortingOptions = sort
                };

                var result = await rolesService.GetRoles(query, new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Add new role
        /// </summary>
        /// <remarks>
        /// Add new role
        /// </remarks>
        /// <param name="request">Information of the role to add</param>
        /// <response code="201">Indicates the role is successfully created.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddRole([FromBody] RoleRequest request)
        {
            try
            {
                var response = await rolesService.AddRole(request, new CancellationToken());

                return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }

        /// <summary>
        /// Update a role
        /// </summary>
        /// <remarks>
        /// Update a role
        /// </remarks>
        /// <param name="request">role details of the role</param>
        /// <param name="roleId">role id of the roles</param>
        /// <response code="204">Indicates the role details is updated</response>
        [HttpPut("{roleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRole(int roleId, [FromBody] UpdateRoleRequestModel request)
        {
            try
            {
                await rolesService.UpdateRole(roleId, request, new CancellationToken());

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Get role details 
        /// </summary>
        ///  /// <remarks>
        /// Get role details based on role id.
        /// </remarks>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationMenuItems))]
        public async Task<ActionResult<ApplicationMenuItems>> RoleDetails(int roleId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await rolesService.RoleDetails(roleId, cancellationToken);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}
