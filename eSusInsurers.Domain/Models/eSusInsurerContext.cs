using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Domain.Models;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppEvent__7944C810F5EE2443");


            entity.ToTable(tb => tb.HasTrigger("trigger_AppEvents_AU"));

            entity.Property(e => e.Id).HasColumnName("EventId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).IsUnicode(false);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<AppEventsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppEvent__5F38963830D31048");


            entity.ToTable("AppEvents_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EventId);
            entity.Property(e => e.EventName).IsUnicode(false);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ApplicationChildMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__0BE735C316236FA9");


            entity.ToTable("ApplicationChildMenu");

            entity.Property(e => e.Id).HasColumnName("ApplicationChildMenuId");
            entity.Property(e => e.ApplicationChildMenuIcon).HasMaxLength(500);
            entity.Property(e => e.ApplicationChildMenuIcon2).HasMaxLength(500);
            entity.Property(e => e.ApplicationChildMenuLink).HasMaxLength(500);
            entity.Property(e => e.ApplicationChildMenuName).HasMaxLength(100);
            entity.Property(e => e.ApplicationMenuId);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate);
            entity.Property(e => e.Sequence);

            entity.HasOne(d => d.ApplicationMenu).WithMany(p => p.ApplicationChildMenus)
                .HasForeignKey(d => d.ApplicationMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Applicati__Appli__567ED357");
        });

        modelBuilder.Entity<ApplicationFunctionality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__BDD9B3C4B3731912");


            entity.Property(e => e.Id).HasColumnName("ApplicationFunctionalityId");
            entity.Property(e => e.ApplicationChildMenuId);
            entity.Property(e => e.ApplicationMenuId);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate);
            entity.Property(e => e.Functionality).HasMaxLength(300);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate);

            entity.HasOne(d => d.ApplicationChildMenu).WithMany(p => p.ApplicationFunctionalities)
                .HasForeignKey(d => d.ApplicationChildMenuId)
                .HasConstraintName("FK__Applicati__Appli__58671BC9");

            entity.HasOne(d => d.ApplicationMenu).WithMany(p => p.ApplicationFunctionalities)
                .HasForeignKey(d => d.ApplicationMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Applicati__Appli__5772F790");
        });

        modelBuilder.Entity<ApplicationMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__E37CD6C7CFC1C799");


            entity.ToTable("ApplicationMenu");

            entity.Property(e => e.Id).HasColumnName("ApplicationMenuId");
            entity.Property(e => e.ApplicationMenuIcon).HasMaxLength(500);
            entity.Property(e => e.ApplicationMenuIcon2).HasMaxLength(500);
            entity.Property(e => e.ApplicationMenuLink).HasMaxLength(500);
            entity.Property(e => e.ApplicationMenuName).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate);
            entity.Property(e => e.Sequence);
        });

        modelBuilder.Entity<ApplicationSetting>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ApplicationSettingId");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.Key).HasMaxLength(500);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate);
            entity.Property(e => e.Value);
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Claims__EF2E139B6F0F8FA1");


            entity.ToTable(tb => tb.HasTrigger("trigger_Claims_AU"));

            entity.Property(e => e.Id).HasColumnName("ClaimId");
            entity.Property(e => e.AllowedAmount).HasColumnType("money");
            entity.Property(e => e.ClaimNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropInsuranceId);
            entity.Property(e => e.CropInsurancePremiumId);
            entity.Property(e => e.Currency)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DisallowAmount).HasColumnType("money");
            entity.Property(e => e.FarmerId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OtherChargesAmount).HasColumnType("money");
            entity.Property(e => e.PaidDate).HasColumnType("datetime");
            entity.Property(e => e.RequestedOn).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CropInsurance).WithMany(p => p.Claims)
                .HasForeignKey(d => d.CropInsuranceId)
                .HasConstraintName("FK_CropInsurances_Claims");

            entity.HasOne(d => d.CropInsurancePremium).WithMany(p => p.Claims)
                .HasForeignKey(d => d.CropInsurancePremiumId)
                .HasConstraintName("FK_CropInsurancePremiums_Claims");

            entity.HasOne(d => d.Farmer).WithMany(p => p.Claims)
                .HasForeignKey(d => d.FarmerId)
                .HasConstraintName("FK_Farmers_Claims");
        });

        modelBuilder.Entity<ClaimsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Claims_A__5F3896387C46B954");


            entity.ToTable("Claims_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.AllowedAmount).HasColumnType("money");
            entity.Property(e => e.ClaimId);
            entity.Property(e => e.ClaimNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropInsuranceId);
            entity.Property(e => e.CropInsurancePremiumId);
            entity.Property(e => e.Currency)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DisallowAmount).HasColumnType("money");
            entity.Property(e => e.FarmerId);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OtherChargesAmount).HasColumnType("money");
            entity.Property(e => e.PaidDate).HasColumnType("datetime");
            entity.Property(e => e.RequestedOn).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CropInsurance).WithMany(p => p.ClaimsAus)
                .HasForeignKey(d => d.CropInsuranceId)
                .HasConstraintName("FK_CropInsurances_Claims_AU");

            entity.HasOne(d => d.CropInsurancePremium).WithMany(p => p.ClaimsAus)
                .HasForeignKey(d => d.CropInsurancePremiumId)
                .HasConstraintName("FK_CropInsurancePremiums_Claims_AU");

            entity.HasOne(d => d.Farmer).WithMany(p => p.ClaimsAus)
                .HasForeignKey(d => d.FarmerId)
                .HasConstraintName("FK_Farmers_Claims_AU");
        });

        modelBuilder.Entity<CountriesAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__5F389638EAA36072");


            entity.ToTable("Countries_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CountryId);
            entity.Property(e => e.CountryName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__10D1609F96CFAFE2");


            entity.ToTable(tb => tb.HasTrigger("trigger_Countries_AU"));

            entity.Property(e => e.Id).HasColumnName("CountryId");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CountryName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Crop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Crops__92356115E929DB23");


            entity.ToTable(tb => tb.HasTrigger("trigger_Crops_AU"));

            entity.Property(e => e.Id).HasColumnName("CropId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropCategoryId);
            entity.Property(e => e.CropName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.MinOrderQuantity).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.QuantityUnits).HasDefaultValue(1);
            entity.Property(e => e.SequenceId);

            entity.HasOne(d => d.CropCategory).WithMany(p => p.Crops)
                .HasForeignKey(d => d.CropCategoryId)
                .HasConstraintName("FK__Crops__CropCateg__36470DEF");
        });

        modelBuilder.Entity<CropCategoriesAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CropCate__5F389638F58D33A9");


            entity.ToTable("CropCategories_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropCategoryId);
            entity.Property(e => e.CropCategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ParentCategoryId);
            entity.Property(e => e.SequenceId);
        });

        modelBuilder.Entity<CropCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CropCate__EA6546C4C67F2FE7");


            entity.ToTable(tb => tb.HasTrigger("trigger_CropCategories_AU"));

            entity.Property(e => e.Id).HasColumnName("CropCategoryId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropCategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ParentCategoryId);
            entity.Property(e => e.SequenceId);
        });

        modelBuilder.Entity<CropInsurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CropInsu__61690A0DCA9B17A4");


            entity.ToTable("CropInsurance", tb => tb.HasTrigger("trigger_CropInsurance_AU"));

            entity.Property(e => e.Id).HasColumnName("CropInsuranceId");
            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FarmerCropId);
            entity.Property(e => e.FarmerId);
            entity.Property(e => e.InsurancePolicyId);
            entity.Property(e => e.InsuranceRiskId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.Latitude).HasColumnType("decimal(8, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.FarmerCrop).WithMany(p => p.CropInsurances)
                .HasForeignKey(d => d.FarmerCropId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FarmerCrops_CropInsurance");

            entity.HasOne(d => d.Farmer).WithMany(p => p.CropInsurances)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Farmers_CropInsurance");

            entity.HasOne(d => d.InsurancePolicy).WithMany(p => p.CropInsurances)
                .HasForeignKey(d => d.InsurancePolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePolicies_CropInsurance");

            entity.HasOne(d => d.InsuranceRisk).WithMany(p => p.CropInsurances)
                .HasForeignKey(d => d.InsuranceRiskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsuranceRisk_CropInsurance");
        });

        modelBuilder.Entity<CropInsuranceAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CropInsu__5F389638B262D123");


            entity.ToTable("CropInsurance_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropInsuranceId);
            entity.Property(e => e.CropName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FarmerCropId);
            entity.Property(e => e.FarmerId);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsurancePolicyId);
            entity.Property(e => e.InsuranceRiskId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.Latitude).HasColumnType("decimal(8, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.FarmerCrop).WithMany(p => p.CropInsuranceAus)
                .HasForeignKey(d => d.FarmerCropId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FarmerCrops_CropInsurance_AU");

            entity.HasOne(d => d.Farmer).WithMany(p => p.CropInsuranceAus)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Farmers_CropInsurance_AU");

            entity.HasOne(d => d.InsurancePolicy).WithMany(p => p.CropInsuranceAus)
                .HasForeignKey(d => d.InsurancePolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePolicies_CropInsurance_AU");

            entity.HasOne(d => d.InsuranceRisk).WithMany(p => p.CropInsuranceAus)
                .HasForeignKey(d => d.InsuranceRiskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsuranceRisk_CropInsurance_AU");
        });

        modelBuilder.Entity<CropInsuranceCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CropInsu__19093A0B53ECEF8C");


            entity.ToTable("CropInsuranceCategory", tb => tb.HasTrigger("trg_cropinsurancecategory_update"));

            entity.Property(e => e.Id).HasColumnName("CategoryId");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompanyId);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.CropInsuranceCategories)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__CropInsur__Compa__04459E07");
        });

        modelBuilder.Entity<CropInsuranceCategoryAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CropInsu__A17F23987788E9FB");


            entity.ToTable("CropInsuranceCategory_Au");

            entity.Property(e => e.Id).HasColumnName("AuditId");
            entity.Property(e => e.ActionTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ActionType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CategoryId);
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompanyId);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CropInsurancePremium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CropInsu__EB982450150CD06B");


            entity.ToTable(tb => tb.HasTrigger("trigger_CropInsurancePremiums_AU"));

            entity.Property(e => e.Id).HasColumnName("CropInsurancePremiumId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropInsuranceId);
            entity.Property(e => e.InsurancePremiumId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PremiumFrequencyId);

            entity.HasOne(d => d.CropInsurance).WithMany(p => p.CropInsurancePremia)
                .HasForeignKey(d => d.CropInsuranceId)
                .HasConstraintName("FK_CropInsurance_CropInsurancePremiums");

            entity.HasOne(d => d.InsurancePremium).WithMany(p => p.CropInsurancePremia)
                .HasForeignKey(d => d.InsurancePremiumId)
                .HasConstraintName("FK_InsurancePremium_CropInsurancePremiums");

            entity.HasOne(d => d.PremiumFrequency).WithMany(p => p.CropInsurancePremia)
                .HasForeignKey(d => d.PremiumFrequencyId)
                .HasConstraintName("FK_PremiumFrequency_CropInsurancePremiums");
        });

        modelBuilder.Entity<CropInsurancePremiumsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CropInsu__5F3896381AF0D3F9");


            entity.ToTable("CropInsurancePremiums_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropInsuranceId);
            entity.Property(e => e.CropInsurancePremiumId);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsurancePremiumId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PremiumFrequencyId);

            entity.HasOne(d => d.CropInsurance).WithMany(p => p.CropInsurancePremiumsAus)
                .HasForeignKey(d => d.CropInsuranceId)
                .HasConstraintName("FK_CropInsurance_CropInsurancePremiums_AU");

            entity.HasOne(d => d.InsurancePremium).WithMany(p => p.CropInsurancePremiumsAus)
                .HasForeignKey(d => d.InsurancePremiumId)
                .HasConstraintName("FK_InsurancePremium_CropInsurancePremiums_AU");

            entity.HasOne(d => d.PremiumFrequency).WithMany(p => p.CropInsurancePremiumsAus)
                .HasForeignKey(d => d.PremiumFrequencyId)
                .HasConstraintName("FK_PremiumFrequency_CropInsurancePremiums_AU");
        });

        modelBuilder.Entity<CropsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Crops_AU__5F389638E1FA4A86");


            entity.ToTable("Crops_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropCategoryId);
            entity.Property(e => e.CropId);
            entity.Property(e => e.CropName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.MinOrderQuantity).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.QuantityUnits).HasDefaultValue(1);
            entity.Property(e => e.SequenceId);

            entity.HasOne(d => d.CropCategory).WithMany(p => p.CropsAus)
                .HasForeignKey(d => d.CropCategoryId)
                .HasConstraintName("FK__Crops_AU__CropCa__3B0BC30C");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__District__85FDA4C69C6E45B1");


            entity.ToTable(tb => tb.HasTrigger("trigger_Districts_AU"));

            entity.Property(e => e.Id).HasColumnName("DistrictId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RegionId);

            entity.HasOne(d => d.Region).WithMany(p => p.Districts)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Regions_Districts");
        });

        modelBuilder.Entity<DistrictsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__District__5F389638EF323A8C");


            entity.ToTable("Districts_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictId);
            entity.Property(e => e.DistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RegionId);

            entity.HasOne(d => d.Region).WithMany(p => p.DistrictsAus)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Regions_Districts_AU");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailTemplates__TemplateId");


            entity.ToTable(tb => tb.HasTrigger("trigger_EmailTemplates_AU"));

            entity.Property(e => e.Id).HasColumnName("TemplateId");
            entity.Property(e => e.Bcc).IsUnicode(false);
            entity.Property(e => e.Cc).IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EventId);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MailContent).IsUnicode(false);
            entity.Property(e => e.MailSubject)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MailTo).IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleId);

            entity.HasOne(d => d.Event).WithMany(p => p.EmailTemplates)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__EmailTemplates__EventId");

            entity.HasOne(d => d.Role).WithMany(p => p.EmailTemplates)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_EmailTemplates_RoleId");
        });

        modelBuilder.Entity<EmailTemplatesAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailTemplates__TemplateId_AU");


            entity.ToTable("EmailTemplates_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.Bcc).IsUnicode(false);
            entity.Property(e => e.Cc).IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EventId);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MailContent).IsUnicode(false);
            entity.Property(e => e.MailSubject)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MailTo).IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleId);
            entity.Property(e => e.TemplateId);

            entity.HasOne(d => d.Event).WithMany(p => p.EmailTemplatesAus)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__EmailTemplates__EventId_AU");

            entity.HasOne(d => d.Role).WithMany(p => p.EmailTemplatesAus)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_EmailTemplates_RoleId_AU");
        });

        modelBuilder.Entity<Farmer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Farmers__731B8888BD364271");


            entity.ToTable(tb => tb.HasTrigger("trigger_Farmers_AU"));

            entity.Property(e => e.Id).HasColumnName("FarmerId");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.AdminComments)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ChiefName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Comments).IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CountryId);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DataConfirmation);
            entity.Property(e => e.DipTank)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.EnterProgramName).IsUnicode(false);
            entity.Property(e => e.FarmSize).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.FarmingCommodity);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IdbackView)
                .IsUnicode(false)
                .HasColumnName("IDBackView");
            entity.Property(e => e.IdfrontView)
                .IsUnicode(false)
                .HasColumnName("IDFrontView");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.IsFarmerDeletedbySuperAdmin).HasColumnName("isFarmerDeletedbySuperAdmin");
            entity.Property(e => e.IsSuspended).HasColumnName("isSuspended");
            entity.Property(e => e.Latitude).HasColumnType("decimal(20, 10)");
            entity.Property(e => e.LevelOfEducation)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Location).IsUnicode(false);
            entity.Property(e => e.Longitude).HasColumnType("decimal(20, 10)");
            entity.Property(e => e.MainOrTraditionalLand);
            entity.Property(e => e.MobileNumber).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.MobilePin)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("MobilePIN");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Msisdn)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("MSISDN");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NearestMountain)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NearestPostOffice)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture).IsUnicode(false);
            entity.Property(e => e.ProgramId);
            entity.Property(e => e.ProgramName);
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RiverName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ServiceProvider).HasMaxLength(100);
            entity.Property(e => e.StreetName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TnCaccepted).HasColumnName("TnCAccepted");
            entity.Property(e => e.VillageName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FarmerCrop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FarmerCr__56CFA48E155F5C1D");


            entity.ToTable(tb => tb.HasTrigger("trigger_FarmerCrops_AU"));

            entity.Property(e => e.Id).HasColumnName("FarmerCropId");
            entity.Property(e => e.Comments);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropId);
            entity.Property(e => e.CultEndDate).HasColumnType("datetime");
            entity.Property(e => e.CultStartDate).HasColumnType("datetime");
            entity.Property(e => e.ESusPoints).HasColumnName("eSusPoints");
            entity.Property(e => e.FarmLandSize).HasColumnType("decimal(10, 5)");
            entity.Property(e => e.FarmerId);
            entity.Property(e => e.HarvestingEndDate).HasColumnType("datetime");
            entity.Property(e => e.HarvestingStartDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.IsCultCompleted);
            entity.Property(e => e.IsHarvestCompleted).HasDefaultValue(false);
            entity.Property(e => e.IsPlantGrowthCompleted).HasDefaultValue(false);
            entity.Property(e => e.IsPrecultCompleted);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PlantGrowthEndDate).HasColumnType("datetime");
            entity.Property(e => e.PlantGrowthStartDate).HasColumnType("datetime");
            entity.Property(e => e.PreCultEndDate).HasColumnType("datetime");
            entity.Property(e => e.PreCultStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Crop).WithMany(p => p.FarmerCrops)
                .HasForeignKey(d => d.CropId)
                .HasConstraintName("FK__FarmerCro__CropI__08B54D69");

            entity.HasOne(d => d.Farmer).WithMany(p => p.FarmerCrops)
                .HasForeignKey(d => d.FarmerId)
                .HasConstraintName("FK__FarmerCro__Farme__07C12930");
        });

        modelBuilder.Entity<FarmerCropsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FarmerAUCr__56CFA48E155F5C1D");


            entity.ToTable("FarmerCrops_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.Comments);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropId);
            entity.Property(e => e.CultEndDate).HasColumnType("datetime");
            entity.Property(e => e.CultStartDate).HasColumnType("datetime");
            entity.Property(e => e.ESusPoints).HasColumnName("eSusPoints");
            entity.Property(e => e.FarmLandSize).HasColumnType("decimal(10, 5)");
            entity.Property(e => e.FarmerCropId);
            entity.Property(e => e.FarmerId);
            entity.Property(e => e.HarvestingEndDate).HasColumnType("datetime");
            entity.Property(e => e.HarvestingStartDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.IsCultCompleted);
            entity.Property(e => e.IsHarvestCompleted);
            entity.Property(e => e.IsPlantGrowthCompleted);
            entity.Property(e => e.IsPrecultCompleted);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PlantGrowthEndDate).HasColumnType("datetime");
            entity.Property(e => e.PlantGrowthStartDate).HasColumnType("datetime");
            entity.Property(e => e.PreCultEndDate).HasColumnType("datetime");
            entity.Property(e => e.PreCultStartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<FarmersAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Farmers___5F38963857558BB3");


            entity.ToTable("Farmers_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.AdminComments)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ChiefName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Comments).IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CountryId);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DataConfirmation);
            entity.Property(e => e.DipTank)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.EnterProgramName).IsUnicode(false);
            entity.Property(e => e.FarmSize).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.FarmerId);
            entity.Property(e => e.FarmingCommodity);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdbackView)
                .IsUnicode(false)
                .HasColumnName("IDBackView");
            entity.Property(e => e.IdfrontView)
                .IsUnicode(false)
                .HasColumnName("IDFrontView");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.IsFarmerDeletedbySuperAdmin).HasColumnName("isFarmerDeletedbySuperAdmin");
            entity.Property(e => e.IsSuspended).HasColumnName("isSuspended");
            entity.Property(e => e.Latitude).HasColumnType("decimal(20, 10)");
            entity.Property(e => e.LevelOfEducation)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Location).IsUnicode(false);
            entity.Property(e => e.Longitude).HasColumnType("decimal(20, 10)");
            entity.Property(e => e.MainOrTraditionalLand);
            entity.Property(e => e.MobileNumber).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.MobilePin)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("MobilePIN");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Msisdn)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("MSISDN");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NearestMountain)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NearestPostOffice)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture).IsUnicode(false);
            entity.Property(e => e.ProgramId);
            entity.Property(e => e.ProgramName);
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RiverName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ServiceProvider).HasMaxLength(100);
            entity.Property(e => e.StreetName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TnCaccepted).HasColumnName("TnCAccepted");
            entity.Property(e => e.VillageName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Features__82230BC9A0EFB064");


            entity.ToTable(tb => tb.HasTrigger("trigger_Features_AU"));

            entity.Property(e => e.Id).HasColumnName("FeatureId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Feature1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Feature");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<FunctionalitiesAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Function__5F3896385157E66F");


            entity.ToTable("Functionalities_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FeatureId);
            entity.Property(e => e.Functionality)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FunctionalityId);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Feature).WithMany(p => p.FunctionalitiesAus)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Features_AU");
        });

        modelBuilder.Entity<Functionality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Function__722E3CFEEA16B5C6");


            entity.ToTable(tb => tb.HasTrigger("trigger_Functionalities_AU"));

            entity.Property(e => e.Id).HasColumnName("FunctionalityId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FeatureId);
            entity.Property(e => e.Functionality1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Functionality");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Feature).WithMany(p => p.Functionalities)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Features");
        });

        modelBuilder.Entity<FunctionalityApprovalProcess>(entity =>
        {
            entity.ToTable("FunctionalityApprovalProcess");

            entity.Property(e => e.Id).HasColumnName("FunctionalityApprovalProcessId");
            entity.Property(e => e.ApplicationFunctionalityId);
            entity.Property(e => e.ApprovalProcess).HasMaxLength(500);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate);

            entity.HasOne(d => d.ApplicationFunctionality).WithMany(p => p.FunctionalityApprovalProcesses)
                .HasForeignKey(d => d.ApplicationFunctionalityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<InsuranceCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__2D971CAC94366EC5");


            entity.ToTable("InsuranceCompany", tb => tb.HasTrigger("trg_insurancecompany_update"));

            entity.Property(e => e.Id).HasColumnName("CompanyId");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<InsuranceCompanyAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__A17F2398AB117D6F");


            entity.ToTable("InsuranceCompany_Au");

            entity.Property(e => e.Id).HasColumnName("AuditId");
            entity.Property(e => e.ActionTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ActionType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CompanyId);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<InsurancePoliciesAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__5F3896386A160592");


            entity.ToTable("InsurancePolicies_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsurancePolicyId);
            entity.Property(e => e.InsuranceProviderId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PolicyName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.InsuranceProvider).WithMany(p => p.InsurancePoliciesAus)
                .HasForeignKey(d => d.InsuranceProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsuranceProviders_InsurancePolicy_AU");
        });

        modelBuilder.Entity<InsurancePolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__8D74AD1FF1405620");


            entity.ToTable(tb => tb.HasTrigger("trigger_InsurancePolicies_AU"));

            entity.Property(e => e.Id).HasColumnName("InsurancePolicyId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsuranceProviderId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PolicyName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.InsuranceProvider).WithMany(p => p.InsurancePolicies)
                .HasForeignKey(d => d.InsuranceProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsuranceProviders_InsurancePolicy");
        });

        modelBuilder.Entity<InsurancePolicy1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__8D74AD1F32569A1F");


            entity.ToTable("InsurancePolicy", tb => tb.HasTrigger("trg_insurancepolicy_update"));

            entity.Property(e => e.Id).HasColumnName("InsurancePolicyId");
            entity.Property(e => e.CategoryId);
            entity.Property(e => e.CompanyId);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PolicyName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.InsurancePolicy1s)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Insurance__Categ__090A5324");

            entity.HasOne(d => d.Company).WithMany(p => p.InsurancePolicy1s)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Insurance__Compa__08162EEB");
        });

        modelBuilder.Entity<InsurancePolicyAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__A17F23986DBA7219");


            entity.ToTable("InsurancePolicy_Au");

            entity.Property(e => e.Id).HasColumnName("AuditId");
            entity.Property(e => e.ActionTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ActionType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CategoryId);
            entity.Property(e => e.CompanyId);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsurancePolicyId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PolicyName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InsurancePremium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__F95457D5C446FE53");


            entity.ToTable("InsurancePremium", tb => tb.HasTrigger("trigger_InsurancePremium_AU"));

            entity.Property(e => e.Id).HasColumnName("InsurancePremiumId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsurancePolicyId);
            entity.Property(e => e.InsuranceRiskId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PremiumAmountCurrency).HasColumnType("money");
            entity.Property(e => e.SumInsuredAmount).HasColumnType("money");
            entity.Property(e => e.SumInsuredAmountCurrency).HasColumnType("money");
            entity.Property(e => e.TotalPremiumAmount).HasColumnType("money");

            entity.HasOne(d => d.InsurancePolicy).WithMany(p => p.InsurancePremia)
                .HasForeignKey(d => d.InsurancePolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePolicy_InsurancePremium");

            entity.HasOne(d => d.InsuranceRisk).WithMany(p => p.InsurancePremia)
                .HasForeignKey(d => d.InsuranceRiskId)
                .HasConstraintName("FK_InsuranceRisk_InsurancePremium");
        });

        modelBuilder.Entity<InsurancePremiumAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__5F389638C032938E");


            entity.ToTable("InsurancePremium_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsurancePolicyId);
            entity.Property(e => e.InsurancePremiumId);
            entity.Property(e => e.InsuranceRiskId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PremiumAmountCurrency).HasColumnType("money");
            entity.Property(e => e.SumInsuredAmount).HasColumnType("money");
            entity.Property(e => e.SumInsuredAmountCurrency).HasColumnType("money");
            entity.Property(e => e.TotalPremiumAmount).HasColumnType("money");

            entity.HasOne(d => d.InsurancePolicy).WithMany(p => p.InsurancePremiumAus)
                .HasForeignKey(d => d.InsurancePolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePolicy_InsurancePremium_AU");

            entity.HasOne(d => d.InsuranceRisk).WithMany(p => p.InsurancePremiumAus)
                .HasForeignKey(d => d.InsuranceRiskId)
                .HasConstraintName("FK_InsuranceRisk_InsurancePremium_AU");
        });

        modelBuilder.Entity<InsurancePremiumFrequency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__14EBA19CB4DDCBAF");


            entity.ToTable("InsurancePremiumFrequency", tb => tb.HasTrigger("trigger_InsurancePremiumFrequency_AU"));

            entity.Property(e => e.Id).HasColumnName("PremiumFrequencyId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsurancePremiumId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PremiumFrequency)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.InsurancePremium).WithMany(p => p.InsurancePremiumFrequencies)
                .HasForeignKey(d => d.InsurancePremiumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePremium_InsurancePremiumFrequency");
        });

        modelBuilder.Entity<InsurancePremiumFrequencyAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__5F389638E1F69167");


            entity.ToTable("InsurancePremiumFrequency_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsurancePremiumId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PremiumFrequency)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PremiumFrequencyId);

            entity.HasOne(d => d.InsurancePremium).WithMany(p => p.InsurancePremiumFrequencyAus)
                .HasForeignKey(d => d.InsurancePremiumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePremium_InsurancePremiumFrequency_AU");
        });

        modelBuilder.Entity<InsuranceProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__7E508CE610302A5F");


            entity.ToTable(tb => tb.HasTrigger("trigger_InsuranceProviders_AU"));

            entity.Property(e => e.Id).HasColumnName("InsurerId");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.Comments).IsUnicode(false);
            entity.Property(e => e.ContactNumber1).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.ContactNumber2).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.ContactPersonAlternateContactNumber).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.ContactPersonAlternateEmailId)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ContactPersonEmailId)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ContactPersonMobileNumber).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.ContactPersonName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CountryId);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId1)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.EmailId2)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.HeadOfficeAddress)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.InsurerName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.IsInsurerVerified);
            entity.Property(e => e.Latitude).HasColumnType("decimal(8, 6)");
            entity.Property(e => e.Logo)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TaxIdentificationNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.InsuranceProviders)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Countries");
        });

        modelBuilder.Entity<InsuranceProviderDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__AE9929CD8975B86D");


            entity.ToTable(tb => tb.HasTrigger("trigger_InsuranceProviderDocuments_AU"));

            entity.Property(e => e.Id).HasColumnName("InsuranceProviderDocumentId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DocumentPath).IsUnicode(false);
            entity.Property(e => e.InsurerId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Insurer).WithMany(p => p.InsuranceProviderDocuments)
                .HasForeignKey(d => d.InsurerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsuranceProviders_InsuranceProviderDocuments");
        });

        modelBuilder.Entity<InsuranceProviderDocumentsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__5F389638AF17B6E4");


            entity.ToTable("InsuranceProviderDocuments_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DocumentPath).IsUnicode(false);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsuranceProviderDocumentId);
            entity.Property(e => e.InsurerId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Insurer).WithMany(p => p.InsuranceProviderDocumentsAus)
                .HasForeignKey(d => d.InsurerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsuranceProviders_InsuranceProviderDocuments_AU");
        });

        modelBuilder.Entity<InsuranceProvidersAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__5F389638E67798D3");


            entity.ToTable("InsuranceProviders_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.Comments).IsUnicode(false);
            entity.Property(e => e.ContactNumber1).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.ContactNumber2).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.ContactPersonAlternateContactNumber).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.ContactPersonAlternateEmailId)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ContactPersonEmailId)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ContactPersonMobileNumber).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.ContactPersonName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CountryId);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId1)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.EmailId2)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.HeadOfficeAddress)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsurerId);
            entity.Property(e => e.InsurerName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.IsInsurerVerified);
            entity.Property(e => e.Latitude).HasColumnType("decimal(8, 6)");
            entity.Property(e => e.Logo)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TaxIdentificationNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.InsuranceProvidersAus)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Countries_AU");
        });

        modelBuilder.Entity<InsuranceRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__6E56996F64692F25");


            entity.Property(e => e.Id).HasColumnName("InsuranceRequestId");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropName).HasMaxLength(200);
            entity.Property(e => e.FarmLocationDistrict).HasMaxLength(200);
            entity.Property(e => e.FarmLocationParish).HasMaxLength(200);
            entity.Property(e => e.FarmLocationSubCounty).HasMaxLength(200);
            entity.Property(e => e.FarmLocationVillage).HasMaxLength(200);
            entity.Property(e => e.FarmLocationWard).HasMaxLength(200);
            entity.Property(e => e.FarmerCropId);
            entity.Property(e => e.FarmerId);
            entity.Property(e => e.FarmerName).HasMaxLength(200);
            entity.Property(e => e.HomeLocationDistrict).HasMaxLength(200);
            entity.Property(e => e.HomeLocationParish).HasMaxLength(200);
            entity.Property(e => e.HomeLocationSubCounty).HasMaxLength(200);
            entity.Property(e => e.HomeLocationVillage).HasMaxLength(200);
            entity.Property(e => e.HomeLocationWard).HasMaxLength(200);
            entity.Property(e => e.InsuranceCompanyName).HasMaxLength(500);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.IsFarmLocationSameAsHomeLocation);
            entity.Property(e => e.IsRequestSubmitted);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ProgramName).HasMaxLength(500);
        });

        modelBuilder.Entity<InsuranceRisk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__1332B5FCCA81E9F2");


            entity.ToTable("InsuranceRisk", tb => tb.HasTrigger("trigger_InsuranceRisk_AU"));

            entity.Property(e => e.Id).HasColumnName("InsuranceRiskId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Deductible).HasColumnType("money");
            entity.Property(e => e.DroughtLoss).HasColumnType("money");
            entity.Property(e => e.ExcessRainfallLoss).HasColumnType("money");
            entity.Property(e => e.FinalPayout).HasColumnType("money");
            entity.Property(e => e.InsurancePolicyId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.LocationId);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Payout).HasColumnType("money");
            entity.Property(e => e.SeasonId);
            entity.Property(e => e.TotalLoss).HasColumnType("money");

            entity.HasOne(d => d.InsurancePolicy).WithMany(p => p.InsuranceRisks)
                .HasForeignKey(d => d.InsurancePolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePolicy_InsuranceRisk");

            entity.HasOne(d => d.Location).WithMany(p => p.InsuranceRisks)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Locations_InsuranceRisk");
        });

        modelBuilder.Entity<InsuranceRiskAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__5F38963872EEE764");


            entity.ToTable("InsuranceRisk_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Deductible).HasColumnType("money");
            entity.Property(e => e.DroughtLoss).HasColumnType("money");
            entity.Property(e => e.ExcessRainfallLoss).HasColumnType("money");
            entity.Property(e => e.FinalPayout).HasColumnType("money");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InsurancePolicyId);
            entity.Property(e => e.InsuranceRiskId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.LocationId);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Payout).HasColumnType("money");
            entity.Property(e => e.SeasonId);
            entity.Property(e => e.TotalLoss).HasColumnType("money");

            entity.HasOne(d => d.InsurancePolicy).WithMany(p => p.InsuranceRiskAus)
                .HasForeignKey(d => d.InsurancePolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePolicy_InsuranceRisk_AU");

            entity.HasOne(d => d.Location).WithMany(p => p.InsuranceRiskAus)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Locations_InsuranceRisk_AU");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__E7FEA497CCA3DC99");


            entity.ToTable(tb => tb.HasTrigger("trigger_Locations_AU"));

            entity.Property(e => e.Id).HasColumnName("LocationId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.Latitude)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LocationName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Longitude)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RegionId);
            entity.Property(e => e.SubCountyId);

            entity.HasOne(d => d.District).WithMany(p => p.Locations)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Districts_Locations");

            entity.HasOne(d => d.Region).WithMany(p => p.Locations)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Regions_Locations");

            entity.HasOne(d => d.SubCounty).WithMany(p => p.Locations)
                .HasForeignKey(d => d.SubCountyId)
                .HasConstraintName("FK_SubCounties_Locations");
        });

        modelBuilder.Entity<LocationsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__5F3896384816BFE7");


            entity.ToTable("Locations_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictId);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.Latitude)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LocationId);
            entity.Property(e => e.LocationName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Longitude)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RegionId);
            entity.Property(e => e.SubCountyId);

            entity.HasOne(d => d.District).WithMany(p => p.LocationsAus)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Districts_Locations_AU");

            entity.HasOne(d => d.Region).WithMany(p => p.LocationsAus)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Regions_Locations_AU");

            entity.HasOne(d => d.SubCounty).WithMany(p => p.LocationsAus)
                .HasForeignKey(d => d.SubCountyId)
                .HasConstraintName("FK_SubCounties_Locations_AU");
        });

        modelBuilder.Entity<MenuRoleFunctionalityApprovalProcess>(entity =>
        {
            entity.ToTable("MenuRoleFunctionalityApprovalProcess");

            entity.Property(e => e.Id).HasColumnName("MenuRoleFunctionalityApprovalProcessId");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate);
            entity.Property(e => e.Enable);
            entity.Property(e => e.FunctionalityApprovalProcessId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate);
            entity.Property(e => e.RoleId);

            entity.HasOne(d => d.FunctionalityApprovalProcess).WithMany(p => p.MenuRoleFunctionalityApprovalProcesses)
                .HasForeignKey(d => d.FunctionalityApprovalProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Role).WithMany(p => p.MenuRoleFunctionalityApprovalProcesses).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<MenuRolesFunctionality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MenuRole__074CCD71B3141746");


            entity.Property(e => e.Id).HasColumnName("MenuRolesFunctionalityId");
            entity.Property(e => e.ApplicationFunctionalityId);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate);
            entity.Property(e => e.Enable);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate);
            entity.Property(e => e.RoleId);

            entity.HasOne(d => d.ApplicationFunctionality).WithMany(p => p.MenuRolesFunctionalities)
                .HasForeignKey(d => d.ApplicationFunctionalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MenuRoles__Appli__5C37ACAD");

            entity.HasOne(d => d.Role).WithMany(p => p.MenuRolesFunctionalities)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MenuRoles__RoleI__5D2BD0E6");
        });

        modelBuilder.Entity<MenuRolesPrivilege>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MenuRole__FD265CFDD6BDBA7B");


            entity.Property(e => e.Id).HasColumnName("MenuRolesPrivilegeId");
            entity.Property(e => e.ApplicationChildMenuId);
            entity.Property(e => e.ApplicationMenuId);
            entity.Property(e => e.Create);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate);
            entity.Property(e => e.Delete);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate);
            entity.Property(e => e.Read);
            entity.Property(e => e.RoleId);
            entity.Property(e => e.Update);

            entity.HasOne(d => d.ApplicationChildMenu).WithMany(p => p.MenuRolesPrivileges)
                .HasForeignKey(d => d.ApplicationChildMenuId)
                .HasConstraintName("FK__MenuRoles__Appli__5F141958");

            entity.HasOne(d => d.ApplicationMenu).WithMany(p => p.MenuRolesPrivileges)
                .HasForeignKey(d => d.ApplicationMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MenuRoles__Appli__5E1FF51F");

            entity.HasOne(d => d.Role).WithMany(p => p.MenuRolesPrivileges)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MenuRoles__RoleI__60083D91");
        });

        modelBuilder.Entity<Parish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Parishes__9D996857AA716091");


            entity.Property(e => e.Id).HasColumnName("ParishId");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ParishName).HasMaxLength(500);
            entity.Property(e => e.SubCountyId);

            entity.HasOne(d => d.SubCounty).WithMany(p => p.Parishes)
                .HasForeignKey(d => d.SubCountyId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PaymentMode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__F9599549EA41D054");


            entity.ToTable(tb => tb.HasTrigger("trigger_PaymentModes_AU"));

            entity.Property(e => e.Id).HasColumnName("PaymentModeId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMode1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PaymentMode");
        });

        modelBuilder.Entity<PaymentModesAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__5F389638FC7BC967");


            entity.ToTable("PaymentModes_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaymentModeId);
        });

        modelBuilder.Entity<PremiumPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PremiumP__679D297815A2DF98");


            entity.ToTable(tb => tb.HasTrigger("trigger_PremiumPayments_AU"));

            entity.Property(e => e.Id).HasColumnName("PremiumPaymentId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropInsuranceId);
            entity.Property(e => e.CropInsurncePremiumId);
            entity.Property(e => e.Currency)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModeOfPayment)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaidAmount).HasColumnType("money");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TaxAmount).HasColumnType("money");
            entity.Property(e => e.TotalPaidAmount).HasColumnType("money");

            entity.HasOne(d => d.CropInsurance).WithMany(p => p.PremiumPayments)
                .HasForeignKey(d => d.CropInsuranceId)
                .HasConstraintName("FK_CropInsurance_PremiumPayments");

            entity.HasOne(d => d.CropInsurncePremium).WithMany(p => p.PremiumPayments)
                .HasForeignKey(d => d.CropInsurncePremiumId)
                .HasConstraintName("FK_CropInsurancePremiums_PremiumPayments");
        });

        modelBuilder.Entity<PremiumPaymentsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PremiumP__5F38963879046800");


            entity.ToTable("PremiumPayments_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropInsuranceId);
            entity.Property(e => e.CropInsurancePremiumId);
            entity.Property(e => e.Currency)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModeOfPayment)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaidAmount).HasColumnType("money");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PremiumPaymentId);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TaxAmount).HasColumnType("money");
            entity.Property(e => e.TotalPaidAmount).HasColumnType("money");

            entity.HasOne(d => d.CropInsurance).WithMany(p => p.PremiumPaymentsAus)
                .HasForeignKey(d => d.CropInsuranceId)
                .HasConstraintName("FK_CropInsurance_PremiumPayments_AU");

            entity.HasOne(d => d.CropInsurancePremium).WithMany(p => p.PremiumPaymentsAus)
                .HasForeignKey(d => d.CropInsurancePremiumId)
                .HasConstraintName("FK_CropInsurancePremiums_PremiumPayments_AU");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Programs__752560587B88440F");


            entity.Property(e => e.Id).HasColumnName("ProgramId");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ParishId);
            entity.Property(e => e.ProgramName).HasMaxLength(500);
            entity.Property(e => e.RegionId);
            entity.Property(e => e.SubCountyId);

            entity.HasOne(d => d.District).WithMany(p => p.Programs).HasForeignKey(d => d.DistrictId);

            entity.HasOne(d => d.Parish).WithMany(p => p.Programs).HasForeignKey(d => d.ParishId);

            entity.HasOne(d => d.Region).WithMany(p => p.Programs)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SubCounty).WithMany(p => p.Programs).HasForeignKey(d => d.SubCountyId);
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Regions__ACD844A3D37510A7");


            entity.ToTable(tb => tb.HasTrigger("trigger_Regions_AU"));

            entity.Property(e => e.Id).HasColumnName("RegionId");
            entity.Property(e => e.CountryId);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RegionName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.Regions)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Countries_Regions");
        });

        modelBuilder.Entity<RegionsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Regions___5F3896382C236203");


            entity.ToTable("Regions_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CountryId);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RegionId);
            entity.Property(e => e.RegionName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.RegionsAus)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Countries_Regions_AU");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__8AFACE1AA64719AD");


            entity.Property(e => e.Id).HasColumnName("RoleId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ReportingToId);
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.ReportingTo).WithMany(p => p.InverseReportingTo).HasForeignKey(d => d.ReportingToId);
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seasons__C1814E3868BF64A5");


            entity.Property(e => e.Id).HasColumnName("SeasonId");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SeasonName).HasMaxLength(200);
            entity.Property(e => e.SeasonYear).HasMaxLength(5);
        });

        modelBuilder.Entity<SeasonCutOffDate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeasonCu__8CC0AA5F20ECD371");


            entity.Property(e => e.Id).HasColumnName("SeasonCutOffDateId");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CropCategoryId);
            entity.Property(e => e.CropId);
            entity.Property(e => e.EndDate);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RegionId);
            entity.Property(e => e.SeasonId);
            entity.Property(e => e.StartDate);

            entity.HasOne(d => d.CropCategory).WithMany(p => p.SeasonCutOffDates).HasForeignKey(d => d.CropCategoryId);

            entity.HasOne(d => d.Crop).WithMany(p => p.SeasonCutOffDates).HasForeignKey(d => d.CropId);

            entity.HasOne(d => d.Region).WithMany(p => p.SeasonCutOffDates)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonCutOffDates)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SubCountiesAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubCount__5F3896385659FD34");


            entity.ToTable("SubCounties_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictId);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SubCountyId);
            entity.Property(e => e.SubCountyName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.District).WithMany(p => p.SubCountiesAus)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Districts_SubCounties_AU");
        });

        modelBuilder.Entity<SubCounty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubCount__11B0FF6F27F77664");


            entity.ToTable(tb => tb.HasTrigger("trigger_SubCounties_AU"));

            entity.Property(e => e.Id).HasColumnName("SubCountyId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SubCountyName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.District).WithMany(p => p.SubCounties)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Districts_SubCounties");
        });

        modelBuilder.Entity<SubFunctionalitiesAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubFunct__5F3896385A9B63AB");


            entity.ToTable("SubFunctionalities_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FunctionalityId);
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SubFunctionality)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SubFunctionalityId);

            entity.HasOne(d => d.Functionality).WithMany(p => p.SubFunctionalitiesAus)
                .HasForeignKey(d => d.FunctionalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Functionalities_AU");
        });

        modelBuilder.Entity<SubFunctionality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubFunct__BFAB7EFF988FD313");


            entity.ToTable(tb => tb.HasTrigger("trigger_SubFunctionalities_AU"));

            entity.Property(e => e.Id).HasColumnName("SubFunctionalityId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FunctionalityId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SubFunctionality1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("SubFunctionality");

            entity.HasOne(d => d.Functionality).WithMany(p => p.SubFunctionalities)
                .HasForeignKey(d => d.FunctionalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Functionalities");
        });

        modelBuilder.Entity<TaxComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaxCompo__E333E7C11587A132");


            entity.ToTable(tb => tb.HasTrigger("trigger_TaxComponents_AU"));

            entity.Property(e => e.Id).HasColumnName("TaxComponentId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.TaxComponent1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TaxComponent");
            entity.Property(e => e.Value);
        });

        modelBuilder.Entity<TaxComponentsAu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaxCompo__5F3896381C36D00C");


            entity.ToTable("TaxComponents_AU");

            entity.Property(e => e.Id).HasColumnName("HistoryRowId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.TaxComponent)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TaxComponentId);
            entity.Property(e => e.Value);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__1788CC4CE0CE872F");


            entity.Property(e => e.Id).HasColumnName("UserId");
            entity.Property(e => e.ContactNumber).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.InsurerId);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.IsAgent);
            entity.Property(e => e.IsEnforcePassword);
            entity.Property(e => e.LastLoggedInDate).HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Otp)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OtpExipiryTime).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.RefreshToken).IsUnicode(false);
            entity.Property(e => e.RefreshTokenExpiryTime)
                .HasColumnType("datetime")
                .HasColumnName("RefreshTokenExpiryTime ");
            entity.Property(e => e.ReportingTo);
            entity.Property(e => e.RoleId);

            entity.HasOne(d => d.Insurer).WithMany(p => p.Users)
                .HasForeignKey(d => d.InsurerId)
                .HasConstraintName("FK_Users_InsuranceProviders");

            entity.HasOne(d => d.ReportingToNavigation).WithMany(p => p.InverseReportingToNavigation)
                .HasForeignKey(d => d.ReportingTo)
                .HasConstraintName("FK_Users_Users");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
