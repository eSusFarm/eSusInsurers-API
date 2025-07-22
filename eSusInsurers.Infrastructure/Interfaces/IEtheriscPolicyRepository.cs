using eSusInsurers.Domain.Entities;
using eSusInsurers.Domain.Models;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces;

public interface IEtheriscPolicyRepository: IRepository<EtheriscPolicy>
{
    Task<EtheriscPolicy> GetByPolicyNumber(string PolicyNumber, CancellationToken cancellationToken);
}