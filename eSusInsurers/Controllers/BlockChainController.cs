using System.Web;
using eSusInsurers.Models.Etherisc;
using eSusInsurers.Models.Etherisc.Policy;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Create Etherisc Policy
    /// </summary>
    /// <param name="etheriscService"></param>
    [Route("policy/[controller]/[action]")]
    public class BlockChainController(IEtheriscService etheriscService): BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddPolicyResponse))]
        public async Task<IActionResult> AddPolicy([FromBody] AddPolicyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await etheriscService.AddPolicy(request, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddPolicyResponse))]
        public async Task<IActionResult> AddPolicyMetaData([FromBody] AddPolicyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await etheriscService.AddPolicyMetadata(request, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddPolicyResponse))]
        public async Task<IActionResult> AddBlockChainPolicy([FromBody] AddPolicyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await etheriscService.CreateBlockChainPolicy(request, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateRiskResponse))]
        public async Task<IActionResult> UpdateInsuranceRisk([FromBody] UpdateRiskRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await etheriscService.UpdateRisk(request, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateRiskResponse))]
        public async Task<IActionResult> SyncInsuranceRisk(string queryString, CancellationToken cancellationToken)
        {
            try
            {
                string riskId = HttpUtility.ParseQueryString(queryString).Get("riskId");
                var response = await etheriscService.SyncRisk(riskId, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}

