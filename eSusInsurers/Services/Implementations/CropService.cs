using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Models.Seasons;
using eSusInsurers.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eSusInsurers.Services
{
    public class CropService(IUnitOfWork unitOfWork, IMapper mapper) : ICropService
    {
        public async Task<List<CropModel>?> GetCropsByCropCategoryId(int cropCategoryId, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = CropFilterByCropCategoryId();
            Expression<Func<Crop, bool>> predicate = ExpressionBuilder<Crop>.BuildFilterExpression(filters);

            var query = unitOfWork.CropRepository.GetAll()
               .Where(predicate)
               .ProjectTo<CropModel>(mapper.ConfigurationProvider);

            return await query.ToListAsync(cancellationToken);
        }

        #region Private Methods
        private static Dictionary<string, Models.Common.Filter> CropFilterByCropCategoryId()
        {
            var filters = new Dictionary<string, Models.Common.Filter>();

            Filters.AddFilterIfNotEmpty(filters, "True", "IsActive", SearchOperationEnum.Equal);

            return filters;
        } 
        #endregion
    }
}
