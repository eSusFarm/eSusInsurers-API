using eSusInsurers.Domain;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace eSusInsurers.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly eSusInsurerContext _context;

        private IInsuranceProviderRepository _insuranceProviderRepository;

        private InsuranceProviderDocumentRepository _insuranceProviderDocumentRepository;

        private IUserRepository _userRepository;

        private IEmailTemplateRepository _emailTemplateRepository;

        private IRoleRepository _roleRepository;

        private IMenuRolesPrivilegeRepository _menuRolesPrivilegeRepository;

        private IMenuRolesFunctionalityRepository _menuRolesFunctionalityRepository;

        private IMenuRoleFunctionalityApprovalProcessRepository _menuRoleFunctionalityApprovalProcessRepository;

        private IApplicationMenuRepository _applicationMenuRepository;

        private IApplicationChildMenuRepository _applicationChildMenuRepository;

        private IApplicationFunctionalitiesRepository _applicationFunctionalitiesRepository;

        private IFunctionalityApprovalProcessRepository _functionalityApprovalProcessRepository;

        private IApplicationSettingsRepository _applicationSettingsRepository;

        private IInsuranceProductRepository _insuranceProductRepository;

        private ISeasonRepository _seasonRepository;

        public UnitOfWork(eSusInsurerContext context)
        {
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IInsuranceProviderRepository InsuranceProviderRepository => _insuranceProviderRepository ??= new InsuranceProviderRepository(_context);

        public IInsuranceProviderDocumentRepository InsuranceProviderDocumentRepository => _insuranceProviderDocumentRepository ??= new InsuranceProviderDocumentRepository(_context);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public IEmailTemplateRepository EmailTemplateRepository => _emailTemplateRepository ??= new EmailTemplateRepository(_context);

        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_context);

        public IMenuRolesPrivilegeRepository MenuRolesPrivilegeRepository => _menuRolesPrivilegeRepository ??= new MenuRolesPrivilegeRepository(_context);

        public IMenuRolesFunctionalityRepository MenuRolesFunctionalityRepository => _menuRolesFunctionalityRepository ??= new MenuRolesFunctionalityRepository(_context);

        public IMenuRoleFunctionalityApprovalProcessRepository MenuRoleFunctionalityApprovalProcessRepository => _menuRoleFunctionalityApprovalProcessRepository ??= new MenuRoleFunctionalityApprovalProcessRepository(_context);

        public IApplicationMenuRepository ApplicationMenuRepository => _applicationMenuRepository ??= new ApplicationMenuRepository(_context);

        public IApplicationChildMenuRepository ApplicationChildMenuRepository => _applicationChildMenuRepository ??= new ApplicationChildMenuRepository(_context);

        public IApplicationFunctionalitiesRepository ApplicationFunctionalitiesRepository => _applicationFunctionalitiesRepository ??= new ApplicationFunctionalitiesRepository(_context);

        public IFunctionalityApprovalProcessRepository FunctionalityApprovalProcessRepository => _functionalityApprovalProcessRepository ??= new FunctionalityApprovalProcessRepository(_context);

        public IApplicationSettingsRepository ApplicationSettingsRepository => _applicationSettingsRepository ??= new ApplicationSettingsRepository(_context);

        public IInsuranceProductRepository InsuranceProductRepository => _insuranceProductRepository ??= new InsuranceProductRepository(_context);

        public ISeasonRepository SeasonRepository => _seasonRepository ??= new SeasonRepository(_context);

        public void SaveChanges()
            => _context.SaveChanges();

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync(cancellationToken);


        public IDbContextTransaction BeginTransaction()
        {
            //It will Begin the transaction on the underlying store connection
            return _context.Database.BeginTransaction();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            //It will Begin the transaction on the underlying store connection
            return _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return _context.Database.CommitTransactionAsync(cancellationToken);
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            return _context.Database.RollbackTransactionAsync(cancellationToken);
        }
    }
}
