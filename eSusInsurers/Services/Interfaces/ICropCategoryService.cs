using eSusInsurers.Models;

namespace eSusInsurers.Services
{
    public interface ICropCategoryService
    {
        Task<List<CropCategoryModel>?> GetCropCategories(CancellationToken cancellationToken);
    }
}
