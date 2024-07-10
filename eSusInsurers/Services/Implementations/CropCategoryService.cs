using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Models;
using eSusInsurers.Services.Common;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Services
{
    public class CropCategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICropCategoryService
    {
        public async Task<List<CropCategoryModel>?> GetCropCategories(CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = CropCategoriesFilter();
            Expression<Func<CropCategory, bool>> predicate = ExpressionBuilder<CropCategory>.BuildFilterExpression(filters);

            var query = unitOfWork.CropCategoryRepository.GetAll()
               .Where(predicate)
               .ProjectTo<CropCategoryModel>(mapper.ConfigurationProvider);

            return await query.ToListAsync(cancellationToken);
        }

        #region Private Methods
        private static Dictionary<string, Models.Common.Filter> CropCategoriesFilter()
        {
            var filters = new Dictionary<string, Models.Common.Filter>();

            //Filters.AddFilterIfNotEmpty(filters, "True", "IsActive", SearchOperationEnum.Equal);

            return filters;
        } 
        #endregion
    }
}
