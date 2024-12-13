using System.Threading;
using System.Threading.Tasks;
using eSusInsurers.Models.EsusFarm;

namespace eSusInsurers.Services.Interfaces
{
    public interface IEsusFarmPolicyService
    {
        Task<bool> ProcessPolicyRequestAsync(EsusFarmPolicyRequestDto request, CancellationToken cancellationToken);
    }
}