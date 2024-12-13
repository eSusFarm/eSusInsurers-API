using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace eSusInsurers.Infrastructure.Common
{
    public interface IUnitOfWork
    {
        IInsuranceProviderRepository InsuranceProviderRepository { get; }
        IInsuranceProviderDocumentRepository InsuranceProviderDocumentRepository { get; }
        IUserRepository UserRepository { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }
        IRoleRepository RoleRepository { get; }
        IMenuRolesPrivilegeRepository MenuRolesPrivilegeRepository { get; }
        IMenuRolesFunctionalityRepository MenuRolesFunctionalityRepository { get; }
        IMenuRoleFunctionalityApprovalProcessRepository MenuRoleFunctionalityApprovalProcessRepository { get; }
        IApplicationMenuRepository ApplicationMenuRepository { get; }
        IApplicationChildMenuRepository ApplicationChildMenuRepository { get; }
        IApplicationFunctionalitiesRepository ApplicationFunctionalitiesRepository { get; }
        IFunctionalityApprovalProcessRepository FunctionalityApprovalProcessRepository { get; }
        IApplicationSettingsRepository ApplicationSettingsRepository { get; }
        IInsuranceProductRepository InsuranceProductRepository { get; }
        ICountriesRepository CountriesRepository { get; }
        IDistrictRepository DistrictRepository { get; }
        ISubcountiesRepository SubcountiesRepository { get; }
        IParishRepository ParishRepository { get; }
        IProgramRepository ProgramRepository { get; }
        IInsuranceCompanyRepository InsuranceCompanyRepository { get; }

        ISeasonRepository SeasonRepository { get; }
        ISeasonCutOffDateRepository SeasonCutOffDateRepository { get; }
        ICropRepository CropRepository { get; }
        ICropCategoryRepository CropCategoryRepository { get; }
        
        IInsuranceRequestsRepository InsuranceRequestsRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken);
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        void Commit();
        void Rollback();
        Task CommitAsync(CancellationToken cancellationToken);
        Task RollbackAsync(CancellationToken cancellationToken);
    }
}
