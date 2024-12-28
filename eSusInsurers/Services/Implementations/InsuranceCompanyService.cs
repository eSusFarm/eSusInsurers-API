using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.InsuranceCompany;
using eSusInsurers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Services.Implementations;

public class InsuranceCompanyService(
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IInsuranceCompanyService
{
    public async Task<List<InsuranceCompanyModel>?> GetCompanies(CancellationToken cancellationToken)
    {
        var query = unitOfWork.InsuranceCompanyRepository.GetAll()
            .ProjectTo<InsuranceCompanyModel>(mapper.ConfigurationProvider);

        return await query.ToListAsync(cancellationToken);
    }
}