using System.Threading;
using System.Threading.Tasks;
using eSusInsurers.Models.EsusFarm;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for policy creation on blockchain.
    /// </summary>
    /// <remarks>
    /// Returns an object of type EsusFarmPolicyRequestDto.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class EsusFarmPolicyController : ControllerBase
    {
        private readonly IEsusFarmPolicyService _esusFarmPolicyService;

        public EsusFarmPolicyController(IEsusFarmPolicyService esusFarmPolicyService)
        {
            _esusFarmPolicyService = esusFarmPolicyService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePolicy([FromBody] EsusFarmPolicyRequestDto request, CancellationToken cancellationToken)
        {
            
            try
            {
                var result = await _esusFarmPolicyService.ProcessPolicyRequestAsync(request, cancellationToken);
                return Ok("Policy request processed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Policy request processing failed. Check logs for details.");
            }
        }
    }
}