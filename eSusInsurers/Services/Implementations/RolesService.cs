using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Constants;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Extensions;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Models.Roles.GetRoles;
using eSusInsurers.Models.Roles.RoleDetails;
using eSusInsurers.Services.Common;
using eSusInsurers.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using WMS.Models.Roles;
using WMS.Models.Roles.UpdateRole;

namespace eSusInsurers.Services.Implementations
{
    public class RolesService(IUnitOfWork unitOfWork, IMapper mapper) : IRolesService
    {
        public async Task<List<ApplicationMenuItems>?> GetApplicationMenuItems(CancellationToken cancellationToken)
        {
            var result = await unitOfWork.ApplicationMenuRepository.GetApplicationMenuList(cancellationToken);

            if (result == null)
                throw new ApplicationException();

            return JsonConvert.DeserializeObject<List<ApplicationMenuItems>>(result);
        }

        public async Task<Models.Common.PagedResult<RoleModel>> GetRoles(GetRolesQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, Models.Common.Filter> filters = RolesFilters(request);
            Expression<Func<Role, bool>> predicate = ExpressionBuilder<Role>.BuildFilterExpression(filters);
            Dictionary<string, Models.Common.Filter> paginationFilters = FilterHelper.CreatePaginationFilters(request.pagingOptions);
            (int PageSize, int Page) paginationParams = FilterHelper.GetPaginationParams(paginationFilters);
            Dictionary<string, Models.Common.Filter> orderByFilters = FilterHelper.CreateOrderByFilters(request.sortingOptions);
            var orderByParams = OrderByHelper.GetOrderByParams(orderByFilters);

            var query = unitOfWork.RoleRepository.GetAll(
                   new string[]
                   {
                       "ReportingTo"
                   })
               .Where(predicate)
               .ProjectTo<RoleModel>(mapper.ConfigurationProvider);

            if (!string.IsNullOrEmpty(orderByParams) && orderByFilters.ContainsKey(ApplicationConstants.sortBy) && orderByFilters[ApplicationConstants.sortBy].Value.ToLower() == "descending")
            {
                orderByParams += ApplicationConstants.descending;
            }

            if (!string.IsNullOrEmpty(orderByParams))
            {
                query = query.OrderBy(orderByParams);
            }

            var roles = query.ToPagedResult(paginationParams.Page, paginationParams.PageSize);

            return new Models.Common.PagedResult<RoleModel>
            {
                PageSize = paginationParams.PageSize,
                TotalPages = roles.TotalPages,
                TotalRecordCount = roles.TotalRecordCount,
                Records = roles.Records,
                CurrentPage = paginationParams.Page
            };

        }

        public async Task<long> AddRole(RoleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var role = await unitOfWork.RoleRepository.GetByRoleNameAsync(request.RoleName, cancellationToken);

            if (role != null)
                throw new BadRequestException($"Role name: ({request.RoleName}) already exists.");

            role = mapper.Map<Role>(request);

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var entity = await unitOfWork.RoleRepository.AddAsync(role, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await AddMenuRolesPrivilege(entity.Id, transaction, cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return entity.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }

        public async Task UpdateRole(int roleId, UpdateRoleRequestModel request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            ArgumentNullException.ThrowIfNull(roleId, nameof(roleId));

            var role = await unitOfWork.RoleRepository.GetByIdAsync(roleId, null, false, cancellationToken);

            if (role == null)
                throw new NotFoundException("Role Id doesn't exist.");

            var roleName = await unitOfWork.RoleRepository.GetByRoleNameAsync(roleId, request.RoleName, cancellationToken);

            if (roleName != null)
                throw new BadRequestException($"Role name: ({request.RoleName}) already exists.");

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                mapper.Map(request, role);

                await unitOfWork.RoleRepository.UpdateAsync(role, cancellationToken);

                await UpdateMenuRolesPrivilege(roleId, request.MenuRolesPrivileges, transaction, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<RoleDetails?> RoleDetails(int roleId, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.RoleRepository.GetRoleDetails(roleId, cancellationToken);

            if (result == null)
                throw new ApplicationException();

            var roleDetails = mapper.Map<RoleDetails>(result);

            roleDetails.MenuRolePrivileges = JsonConvert.DeserializeObject<List<MenuRolePrivileges>>(result.ApplicationMenuRolePrivileges);

            return roleDetails;
        }

        private async Task AddMenuRolesPrivilege(int roleId, IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            try
            {
                var result = await unitOfWork.ApplicationMenuRepository.GetApplicationMenuList(cancellationToken);

                if (result == null)
                    throw new ApplicationException();

                var query = JsonConvert.DeserializeObject<List<ApplicationMenuItems>>(result);

                foreach (var menuItem in query)
                {
                    var menuRolesPrivileges = mapper.Map<MenuRolesPrivilege>(menuItem);
                    menuRolesPrivileges.RoleId = roleId;

                    await unitOfWork.MenuRolesPrivilegeRepository.AddAsync(menuRolesPrivileges, cancellationToken);

                    if (menuItem.ApplicationChildMenuItems.Count > 0)
                    {
                        foreach (var childMenuItem in menuItem.ApplicationChildMenuItems)
                        {
                            var childMenuRolesPrivileges = mapper.Map<MenuRolesPrivilege>(childMenuItem);
                            childMenuRolesPrivileges.ApplicationMenuId = menuItem.ApplicationMenuId;
                            childMenuRolesPrivileges.RoleId = roleId;

                            await unitOfWork.MenuRolesPrivilegeRepository.AddAsync(childMenuRolesPrivileges, cancellationToken);

                            if (childMenuItem.ApplicationFunctionalities.Count > 0)
                            {
                                foreach (var applicationFunctionality in childMenuItem.ApplicationFunctionalities)
                                {
                                    var menuRolesFunctionality = mapper.Map<MenuRolesFunctionality>(applicationFunctionality);
                                    menuRolesFunctionality.RoleId = roleId;

                                    await unitOfWork.MenuRolesFunctionalityRepository.AddAsync(menuRolesFunctionality, cancellationToken);

                                    foreach (var approvalProcesses in applicationFunctionality.FunctionalityApprovalProcessList)
                                    {
                                        var functionalityApprovalProcesses = mapper.Map<Domain.Entities.MenuRoleFunctionalityApprovalProcess>(approvalProcesses);
                                        functionalityApprovalProcesses.RoleId = roleId;

                                        await unitOfWork.MenuRoleFunctionalityApprovalProcessRepository.AddAsync(functionalityApprovalProcesses, cancellationToken);
                                    }
                                }
                            }
                        }
                    }

                    if (menuItem.ApplicationFunctionalities.Count > 0)
                    {
                        foreach (var applicationFunctionality in menuItem.ApplicationFunctionalities)
                        {
                            var menuRolesFunctionality = mapper.Map<MenuRolesFunctionality>(applicationFunctionality);
                            menuRolesFunctionality.RoleId = roleId;

                            await unitOfWork.MenuRolesFunctionalityRepository.AddAsync(menuRolesFunctionality, cancellationToken);

                            foreach (var approvalProcesses in applicationFunctionality.FunctionalityApprovalProcessList)
                            {
                                var functionalityApprovalProcesses = mapper.Map<Domain.Entities.MenuRoleFunctionalityApprovalProcess>(approvalProcesses);
                                functionalityApprovalProcesses.RoleId = roleId;

                                await unitOfWork.MenuRoleFunctionalityApprovalProcessRepository.AddAsync(functionalityApprovalProcesses, cancellationToken);
                            }
                        }
                    }
                }

                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }

        private async Task UpdateMenuRolesPrivilege(int roleId, List<MenuRolesPrivilegeItems> menuRolesPrivilegeItems, IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            try
            {
                var menuRolesPrivileges = await unitOfWork.MenuRolesPrivilegeRepository.GetAllRoleIdAsync(roleId, cancellationToken);

                var menuRolesFunctionalities = await unitOfWork.MenuRolesFunctionalityRepository.GetAllRoleIdAsync(roleId, cancellationToken);

                var menuRoleFunctionalityApprovals = await unitOfWork.MenuRoleFunctionalityApprovalProcessRepository.GetAllByRoleIdAsync(roleId, cancellationToken);

                foreach (var menuItem in menuRolesPrivilegeItems)
                {
                    var menuRolesPrivilege = menuRolesPrivileges.Where(x => x.Id == menuItem.MenuRolesPrivilegeId).FirstOrDefault();
                    if (menuRolesPrivilege != null)
                    {
                        mapper.Map(menuItem, menuRolesPrivilege);
                        menuRolesPrivilege.RoleId = roleId;

                        await unitOfWork.MenuRolesPrivilegeRepository.UpdateAsync(menuRolesPrivilege, cancellationToken);
                    }

                    if (menuItem.MenuRolesFunctionalities.Count > 0)
                    {
                        foreach (var menuFunctionality in menuItem.MenuRolesFunctionalities)
                        {
                            var functionality = menuRolesFunctionalities.Where(x => x.Id == menuFunctionality.MenuRolesFunctionalityId).FirstOrDefault();
                            if (functionality != null)
                            {
                                mapper.Map(menuFunctionality, functionality);
                                functionality.RoleId = roleId;

                                await unitOfWork.MenuRolesFunctionalityRepository.UpdateAsync(functionality, cancellationToken);
                            }

                            if (menuFunctionality.MenuRoleFunctionalityApprovalProcess.Count > 0)
                            {
                                foreach (var applicationFunctionality in menuFunctionality.MenuRoleFunctionalityApprovalProcess)
                                {
                                    var menuRoleFunctionality = menuRoleFunctionalityApprovals.Where(x => x.Id == applicationFunctionality.MenuRoleFunctionalityApprovalProcessId).FirstOrDefault();
                                    if (menuRoleFunctionality != null)
                                    {
                                        mapper.Map(applicationFunctionality, menuRoleFunctionality);
                                        menuRoleFunctionality.RoleId = roleId;

                                        await unitOfWork.MenuRoleFunctionalityApprovalProcessRepository.UpdateAsync(menuRoleFunctionality, cancellationToken);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }

        #region Private Methods
        private static Dictionary<string, Models.Common.Filter> RolesFilters(GetRolesQuery invQuery)
        {
            var inboundDto = invQuery.filter;
            var filters = new Dictionary<string, Models.Common.Filter>();

            if (inboundDto != null)
            {
                if (inboundDto?.IsActive != null)
                    Filters.AddFilterIfNotEmpty(filters, inboundDto.IsActive == true ? "True" : "False", "IsActive", SearchOperationEnum.Equal);

            }

            return filters;
        }
        #endregion
    }
}
