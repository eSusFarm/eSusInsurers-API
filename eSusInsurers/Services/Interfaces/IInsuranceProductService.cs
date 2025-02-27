using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceProducts;

namespace eSusInsurers.Services.Interfaces;

public interface IInsuranceProductService
{
    Task<PagedResult<InsuranceProductModel>> GetInsuranceProducts(InsuranceProductQuery request,
        CancellationToken cancellationToken);
}