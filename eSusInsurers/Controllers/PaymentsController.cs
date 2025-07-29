using eSusInsurers.Models.Etherisc.Policy;
using eSusInsurers.Models.Payment;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Create Etherisc Policy
    /// </summary>
    /// <param name="etheriscService"></param>
    [Route("add_payment")]
    public class PaymentsController(IPaymentService paymentService): BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterPaymentResponse))]
        public async Task<IActionResult> AddPolicy([FromBody] RegisterPaymentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response =await  paymentService.registerPayment(request, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }   
}

