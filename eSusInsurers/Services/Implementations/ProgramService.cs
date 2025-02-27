using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Constants;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Extensions;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Models.Programs;
using eSusInsurers.Services.Common;
using eSusInsurers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Services.Implementations;

public class ProgramService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IProgramsService
{
    public async Task<Models.Common.PagedResult<ProgramsModel>> GetPrograms(GetProgramsQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Models.Common.Filter> filters = ProgarmsFilters(request);
        Expression<Func<Domain.Entities.Program, bool>> predicate =
            ExpressionBuilder<Domain.Entities.Program>.BuildFilterExpression(filters);
        Dictionary<string, Models.Common.Filter> paginationFilters =
            FilterHelper.CreatePaginationFilters(request.pagingOptions);
        var paginationParams = FilterHelper.GetPaginationParams(paginationFilters);
        Dictionary<string, Models.Common.Filter> orderByFilters =
            FilterHelper.CreateOrderByFilters(request.sortingOptions);
        var orderByParams = OrderByHelper.GetOrderByParams(orderByFilters);

        var query = unitOfWork.ProgramRepository.GetAll(
                new[]
                {
                    "Region", "District", "SubCounty", "Parish"
                })
            .Where(predicate)
            .OrderByDescending(x => x.Id)
            .ProjectTo<ProgramsModel>(mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(orderByParams) && orderByFilters.ContainsKey(ApplicationConstants.sortBy) &&
            orderByFilters[ApplicationConstants.sortBy].Value.ToLower() == "descending")
            orderByParams += ApplicationConstants.descending;

        if (!string.IsNullOrEmpty(orderByParams)) query = query.OrderBy(orderByParams);

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

        var program = await unitOfWork.ProgramRepository.GetAll()
            .Where(x => x.ProgramName == request.ProgramName)
            .FirstOrDefaultAsync(cancellationToken);

        if (program != null)
            throw new Exception($"Program Name ({request.ProgramName}) already exists.");

        program = mapper.Map<Domain.Entities.Program>(request);

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

    public async Task UpdateProgram(int programId, ProgramRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        ArgumentNullException.ThrowIfNull(programId, nameof(programId));

        var programs = await unitOfWork.ProgramRepository.GetAll()
            .Where(x => x.ProgramName == request.ProgramName)
            .ToListAsync(cancellationToken);

        if (programs.Any(x => x.Id != programId))
            throw new BadRequestException("Program Name and Institution Name already exists.");

        var program = await unitOfWork.ProgramRepository.GetByIdAsync(programId, cancellationToken: cancellationToken);

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
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }

    public async Task DeleteProgram(int programId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(programId, nameof(programId));

        var program = await unitOfWork.ProgramRepository.GetByIdAsync(programId, cancellationToken: cancellationToken);

        if (program == null)
            throw new NotFoundException($"Program Id: ({programId}) doesn't exist.");

        program.IsActive = false;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task ActivateProgram(int programId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(programId, nameof(programId));

        var program = await unitOfWork.ProgramRepository.GetByIdAsync(programId, cancellationToken: cancellationToken);

        if (program == null)
            throw new NotFoundException($"Program Id: ({programId}) doesn't exist.");

        program.IsActive = true;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }


    private static Dictionary<string, Models.Common.Filter> ProgarmsFilters(GetProgramsQuery invQuery)
    {
        var inboundDto = invQuery.filter;
        var filters = new Dictionary<string, Models.Common.Filter>();

        if (inboundDto != null)
        {
            Filters.AddFilterIfNotEmpty(filters, inboundDto?.ProgramName, "ProgramName", SearchOperationEnum.Contains);

            Filters.AddFilterIfNotEmpty(filters, inboundDto?.RegionName, "Region.RegionName",
                SearchOperationEnum.Contains);

            Filters.AddFilterIfNotEmpty(filters, inboundDto?.DistrictName, "District.DistrictName",
                SearchOperationEnum.Contains);

            Filters.AddFilterIfNotEmpty(filters, inboundDto?.SubCountyName, "SubCounty.SubCountyName",
                SearchOperationEnum.Contains);

            Filters.AddFilterIfNotEmpty(filters, inboundDto?.ParishName, "Parish.ParishName",
                SearchOperationEnum.Contains);

            Filters.AddFilterIfValueGreaterThanZero(filters, inboundDto?.RegionId, "RegionId",
                SearchOperationEnum.Equal);

            Filters.AddFilterIfValueGreaterThanZero(filters, inboundDto?.DistrictId, "DistrictId",
                SearchOperationEnum.Equal);

            Filters.AddFilterIfValueGreaterThanZero(filters, inboundDto?.SubcountyId, "SubCountyId",
                SearchOperationEnum.Equal);

            Filters.AddFilterIfValueGreaterThanZero(filters, inboundDto?.ParishId, "ParishId",
                SearchOperationEnum.Equal);

            if (inboundDto?.IsActive != null)
                Filters.AddFilterIfNotEmpty(filters, inboundDto.IsActive == true ? "True" : "False", "IsActive",
                    SearchOperationEnum.Equal);
        }

        return filters;
    }
}