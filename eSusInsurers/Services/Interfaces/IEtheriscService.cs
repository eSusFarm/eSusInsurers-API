using eSusInsurers.Models.Etherisc;

namespace eSusInsurers.Services.Interfaces
{
    public interface IEtheriscService
    {
        Task<long> AddPolicy(AddPolicyRequest request, CancellationToken cancellationToken);
    }
}

