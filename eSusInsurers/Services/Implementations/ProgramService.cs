using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Constants;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Extensions;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Models.Programs;
using eSusInsurers.Services.Common;
using eSusInsurers.Services.Interfaces;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Domain.Entities;
using WMS.Models.Roles;
using eSusInsurers.Models.Users.UpdateUser;
using eSusInsurers.Helpers;
using eSusInsurers.Models.Common;
using eSusInsurers.Models;

namespace eSusInsurers.Services.Implementations
{
    public class ProgramService(IUnitOfWork unitOfWork,
                             IConfiguration configuration,
                             IMapper mapper,
                             ITokenService tokenService,
                             IDateTime dateTime,
                             IEmailService emailService) : IProgramsService
    {

        public async Task<Models.Common.PagedResult<ProgramsModel>> GetPrograms(GetProgramsQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = ProgarmsFilters(request);
            Expression<Func<eSusInsurers.Domain.Entities.Program, bool>> predicate = ExpressionBuilder<eSusInsurers.Domain.Entities.Program>.BuildFilterExpression(filters);
            Dictionary<string, Models.Common.Filter> paginationFilters = FilterHelper.CreatePaginationFilters(request.pagingOptions);
            (int PageSize, int Page) paginationParams = FilterHelper.GetPaginationParams(paginationFilters);
            Dictionary<string, Models.Common.Filter> orderByFilters = FilterHelper.CreateOrderByFilters(request.sortingOptions);
            var orderByParams = OrderByHelper.GetOrderByParams(orderByFilters);

            var query = unitOfWork.ProgramRepository.GetAll(
                new string[]
                   {
                        "Region", "District", "SubCounty"
                   })
               .Where(predicate)
               .OrderByDescending(x => x.Id)
               .ProjectTo<ProgramsModel>(mapper.ConfigurationProvider);

            if (!string.IsNullOrEmpty(orderByParams) && orderByFilters.ContainsKey(ApplicationConstants.sortBy) && orderByFilters[ApplicationConstants.sortBy].Value.ToLower() == "descending")
            {
                orderByParams += ApplicationConstants.descending;
            }

            if (!string.IsNullOrEmpty(orderByParams))
            {
                query = query.OrderBy(orderByParams);
            }

            var programs = query.ToPagedResult(paginationParams.Page, paginationParams.PageSize);

            return new Models.Common.PagedResult<ProgramsModel>
            {
                PageSize = paginationParams.PageSize,
                TotalPages = programs.TotalPages,
                TotalRecordCount = programs.TotalRecordCount,
                Records = programs.Records,
                CurrentPage = paginationParams.Page
            };

        }

        public async Task<bool> AddProgram(ProgramRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var program = await unitOfWork.ProgramRepository.GetByProgarmAndInstitutionNameAsync(request.ProgramName, request.InstitutionName, cancellationToken);

            if (program != null)
                throw new Exception($"Program Name ({request.ProgramName}) already exists.");

            program = mapper.Map<eSusInsurers.Domain.Entities.Program>(request);

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await unitOfWork.ProgramRepository.AddAsync(program, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return true;
        }

        public async Task UpdateProgram(int progarm_Id, ProgramRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            ArgumentNullException.ThrowIfNull(progarm_Id, nameof(progarm_Id));

            var program = await unitOfWork.ProgramRepository.GetByIdAsync(progarm_Id, null, false, cancellationToken);

            if (program == null)
                throw new NotFoundException("Program Id doesn't exist.");

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                mapper.Map(request, program);

                await unitOfWork.ProgramRepository.UpdateAsync(program, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }

        public async Task<object> DeleteProgram(int program_Id, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(program_Id, nameof(program_Id));

            var program = await unitOfWork.ProgramRepository.GetByIdAsync(program_Id, null, false, cancellationToken);

            if (program == null)
                throw new NotFoundException($"Program Id: ({program_Id}) doesn't exist.");

            program.IsActive = false;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<object> ActivateProgram(int program_Id, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(program_Id, nameof(program_Id));

            var program = await unitOfWork.ProgramRepository.GetByIdAsync(program_Id, null, false, cancellationToken);

            if (program == null)
                throw new NotFoundException($"Program Id: ({program_Id}) doesn't exist.");

            program.IsActive = true;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }


        private static Dictionary<string, Models.Common.Filter> ProgarmsFilters(GetProgramsQuery invQuery)
        {
            var inboundDto = invQuery.filter;
            var filters = new Dictionary<string, Models.Common.Filter>();

            if (inboundDto != null)
            {

                Filters.AddFilterIfNotEmpty(filters, inboundDto?.Programs, "ProgramName", SearchOperationEnum.Contains);

                Filters.AddFilterIfNotEmpty(filters, inboundDto?.Institutions, "InstitutionName", SearchOperationEnum.Contains);

                Filters.AddFilterIfNotEmpty(filters, inboundDto?.RegionName, "Region.RegionName", SearchOperationEnum.Contains, true);

                Filters.AddFilterIfNotEmpty(filters, inboundDto?.DistrictName, "District.DistrictName", SearchOperationEnum.Contains, true);

                Filters.AddFilterIfNotEmpty(filters, inboundDto?.SubCountyName, "SubCounty.SubCountyName", SearchOperationEnum.Contains, true);

                if (inboundDto?.IsActive != null)
                    Filters.AddFilterIfNotEmpty(filters, inboundDto.IsActive == true ? "True" : "False", "IsActive", SearchOperationEnum.Equal);


            }

            return filters;
        }
    }
}
