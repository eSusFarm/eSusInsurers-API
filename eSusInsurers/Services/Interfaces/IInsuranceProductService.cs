using eSusInsurers.Models.InsuranceProducts;

namespace eSusInsurers.Services.Interfaces
{
    public interface IInsuranceProductService
    {
        Task<Models.Common.PagedResult<InsuranceProductModel>> GetInsuranceProducts(InsuranceProductQuery request, CancellationToken cancellationToken);
    }
}
