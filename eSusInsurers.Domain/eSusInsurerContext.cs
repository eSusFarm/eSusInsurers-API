
using eSusInsurers.Domain.Configurations;
using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace eSusInsurers.Domain;

public partial class eSusInsurerContext : DbContext
{
    public eSusInsurerContext(DbContextOptions<eSusInsurerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppEvent> AppEvents { get; set; }

    public virtual DbSet<AppEventsAu> AppEventsAus { get; set; }

    public virtual DbSet<ApplicationChildMenu> ApplicationChildMenus { get; set; }

    public virtual DbSet<ApplicationFunctionality> ApplicationFunctionalities { get; set; }

    public virtual DbSet<ApplicationMenu> ApplicationMenus { get; set; }

    public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<ClaimsAu> ClaimsAus { get; set; }

    public virtual DbSet<CountriesAu> CountriesAus { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Crop> Crops { get; set; }

    public virtual DbSet<CropCategoriesAu> CropCategoriesAus { get; set; }

    public virtual DbSet<CropCategory> CropCategories { get; set; }

    public virtual DbSet<CropInsurance> CropInsurances { get; set; }

    public virtual DbSet<CropInsuranceAu> CropInsuranceAus { get; set; }

    public virtual DbSet<CropInsuranceCategory> CropInsuranceCategories { get; set; }

    public virtual DbSet<CropInsuranceCategoryAu> CropInsuranceCategoryAus { get; set; }

    public virtual DbSet<CropInsurancePremium> CropInsurancePremiums { get; set; }

    public virtual DbSet<CropInsurancePremiumsAu> CropInsurancePremiumsAus { get; set; }

    public virtual DbSet<CropsAu> CropsAus { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<DistrictsAu> DistrictsAus { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<EmailTemplatesAu> EmailTemplatesAus { get; set; }

    public virtual DbSet<Farmer> Farmers { get; set; }

    public virtual DbSet<FarmerCrop> FarmerCrops { get; set; }

    public virtual DbSet<FarmerCropsAu> FarmerCropsAus { get; set; }

    public virtual DbSet<FarmersAu> FarmersAus { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<FunctionalitiesAu> FunctionalitiesAus { get; set; }

    public virtual DbSet<Functionality> Functionalities { get; set; }

    public virtual DbSet<FunctionalityApprovalProcess> FunctionalityApprovalProcesses { get; set; }

    public virtual DbSet<InsuranceCompany> InsuranceCompanies { get; set; }

    public virtual DbSet<InsuranceCompanyAu> InsuranceCompanyAus { get; set; }

    public virtual DbSet<InsurancePoliciesAu> InsurancePoliciesAus { get; set; }

    public virtual DbSet<InsurancePolicy> InsurancePolicies { get; set; }

    public virtual DbSet<InsurancePolicy1> InsurancePolicies1 { get; set; }

    public virtual DbSet<InsurancePolicyAu> InsurancePolicyAus { get; set; }

    public virtual DbSet<InsurancePremium> InsurancePremia { get; set; }

    public virtual DbSet<InsurancePremiumAu> InsurancePremiumAus { get; set; }

    public virtual DbSet<InsurancePremiumFrequency> InsurancePremiumFrequencies { get; set; }

    public virtual DbSet<InsurancePremiumFrequencyAu> InsurancePremiumFrequencyAus { get; set; }

    public virtual DbSet<InsuranceProvider> InsuranceProviders { get; set; }

    public virtual DbSet<InsuranceProviderDocument> InsuranceProviderDocuments { get; set; }

    public virtual DbSet<InsuranceProviderDocumentsAu> InsuranceProviderDocumentsAus { get; set; }

    public virtual DbSet<InsuranceProvidersAu> InsuranceProvidersAus { get; set; }

    public virtual DbSet<InsuranceRequest> InsuranceRequests { get; set; }

    public virtual DbSet<InsuranceRisk> InsuranceRisks { get; set; }

    public virtual DbSet<InsuranceRiskAu> InsuranceRiskAus { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LocationsAu> LocationsAus { get; set; }

    public virtual DbSet<MenuRoleFunctionalityApprovalProcess> MenuRoleFunctionalityApprovalProcesses { get; set; }

    public virtual DbSet<MenuRolesFunctionality> MenuRolesFunctionalities { get; set; }

    public virtual DbSet<MenuRolesPrivilege> MenuRolesPrivileges { get; set; }

    public virtual DbSet<Parish> Parishes { get; set; }

    public virtual DbSet<PaymentMode> PaymentModes { get; set; }

    public virtual DbSet<PaymentModesAu> PaymentModesAus { get; set; }

    public virtual DbSet<PremiumPayment> PremiumPayments { get; set; }

    public virtual DbSet<PremiumPaymentsAu> PremiumPaymentsAus { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RegionsAu> RegionsAus { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<SeasonCutOffDate> SeasonCutOffDates { get; set; }

    public virtual DbSet<SubCountiesAu> SubCountiesAus { get; set; }

    public virtual DbSet<SubCounty> SubCounties { get; set; }

    public virtual DbSet<SubFunctionalitiesAu> SubFunctionalitiesAus { get; set; }

    public virtual DbSet<SubFunctionality> SubFunctionalities { get; set; }

    public virtual DbSet<TaxComponent> TaxComponents { get; set; }

    public virtual DbSet<TaxComponentsAu> TaxComponentsAus { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<EsusFarmPolicy> EsusFarmPolicy { get; set; }
    public virtual DbSet<EtheriscPolicy> EtheriscPolicy { get; set; }
    public virtual DbSet<InsurancePremiumPayment> InsurancePremiumPayment { get; set; }

    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.AppEventConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.AppEventsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ApplicationChildMenuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ApplicationFunctionalityConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ApplicationMenuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ApplicationSettingConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ClaimConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ClaimsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CountriesAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CountryConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CropConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CropCategoriesAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CropCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CropInsuranceConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CropInsuranceAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CropInsuranceCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CropInsuranceCategoryAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CropInsurancePremiumConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CropInsurancePremiumsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CropsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.DistrictConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.DistrictsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.EmailTemplateConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.EmailTemplatesAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FarmerConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FarmerCropConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FarmerCropsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FarmersAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FeatureConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FunctionalitiesAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FunctionalityConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FunctionalityApprovalProcessConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsuranceCompanyConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsuranceCompanyAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsurancePoliciesAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsurancePolicyConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsurancePolicy1Configuration());
        modelBuilder.ApplyConfiguration(new Configurations.InsurancePolicyAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsurancePremiumConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsurancePremiumAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsurancePremiumFrequencyConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsurancePremiumFrequencyAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsuranceProviderConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsuranceProviderDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsuranceProviderDocumentsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsuranceProvidersAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsuranceRequestConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsuranceRiskConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsuranceRiskAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.LocationConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.LocationsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.MenuRoleFunctionalityApprovalProcessConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.MenuRolesFunctionalityConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.MenuRolesPrivilegeConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ParishConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.PaymentModeConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.PaymentModesAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.PremiumPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.PremiumPaymentsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ProgramConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RegionConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RegionsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RoleConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.SeasonConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.SeasonCutOffDateConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.SubCountiesAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.SubCountyConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.SubFunctionalitiesAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.SubFunctionalityConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.TaxComponentConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.TaxComponentsAuConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FarmerConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.InsurancePremiumPaymentConfiguration());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
