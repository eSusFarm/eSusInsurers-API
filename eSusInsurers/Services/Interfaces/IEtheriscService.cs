using eSusInsurers.Domain.Entities;
using eSusInsurers.Models.Etherisc;
using eSusInsurers.Models.Etherisc.Policy;

namespace eSusInsurers.Services.Interfaces
{
    public interface IEtheriscService
    {
        Task<AddPolicyResponse> AddPolicy(AddPolicyRequest request, CancellationToken cancellationToken);
        Task<AddPolicyResponse> AddPolicyMetadata(AddPolicyRequest request, CancellationToken cancellationToken);
        Task<AddPolicyResponse> CreateBlockChainPolicy(AddPolicyRequest request, CancellationToken cancellationToken);
    }
}

