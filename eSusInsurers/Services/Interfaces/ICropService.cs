using eSusInsurers.Models;

namespace eSusInsurers.Services
{
    public interface ICropService
    {
        Task<List<CropModel>?> GetCropsByCropCategoryId(int cropCategoryId, CancellationToken cancellationToken);
    }
}
