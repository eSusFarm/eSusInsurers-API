using eSusInsurers.Models.Etherisc.Policy;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Create Etherisc Policy
    /// </summary>
    /// <param name="etheriscService"></param>
    [Route("etherisc_add_policy")]
    public class EtheriscController(IEtheriscService etheriscService): BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddPolicyResponse))]
        public IActionResult AddPolicy([FromBody] AddPolicyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = etheriscService.AddPolicy(request, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}

