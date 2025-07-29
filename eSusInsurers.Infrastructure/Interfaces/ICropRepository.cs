using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure
{
    public interface ICropRepository : IRepository<Crop>
    {
       Task<Crop?> GetCropByName(string roleName, CancellationToken cancellationToken);
    }
}
