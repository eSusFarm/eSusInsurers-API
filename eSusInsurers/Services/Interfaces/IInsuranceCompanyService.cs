using eSusInsurers.Models.InsuranceCompany;

namespace eSusInsurers.Services.Interfaces;

public interface IInsuranceCompanyService
{
    Task<List<InsuranceCompanyModel>?> GetCompanies(CancellationToken cancellationToken);
}