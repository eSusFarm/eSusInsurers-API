using eSusInsurers.Models.Countries;
using eSusInsurers.Models.InsuranceCompany;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for insurance companies.
    /// </summary>
    [Route("insurance_companies")]
    public class InsuranceCompanyController(IInsuranceCompanyService insuranceCompanyService) : BaseController
    {
        /// <summary>
        /// Get insurance companies
        /// </summary>
        /// <remarks>
        /// Returns insurance companies.
        /// </remarks>
        /// <response code="200">Returns insurance companies.</response>
        /// <returns>Returns insurance companies</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<InsuranceCompanyModel>))]
        public async Task<ActionResult<List<InsuranceCompanyModel>>> GetInsuranceCompanies()
        {
            try
            {
                var result = await insuranceCompanyService.GetCompanies(new CancellationToken());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}
