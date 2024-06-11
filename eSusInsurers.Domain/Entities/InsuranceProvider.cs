using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class InsuranceProvider : BaseAuditableEntity
{

    public string InsurerName { get; set; } = null!;

    public string? TaxIdentificationNumber { get; set; }

    public string ContactPersonName { get; set; } = null!;

    public decimal ContactPersonMobileNumber { get; set; }

    public decimal? ContactPersonAlternateContactNumber { get; set; }

    public string ContactPersonEmailId { get; set; } = null!;

    public string? ContactPersonAlternateEmailId { get; set; }

    public int CountryId { get; set; }

    public string HeadOfficeAddress { get; set; } = null!;

    public string EmailId1 { get; set; } = null!;

    public string? EmailId2 { get; set; }

    public decimal ContactNumber1 { get; set; }

    public decimal? ContactNumber2 { get; set; }

    public string? Status { get; set; }

    public bool IsInsurerVerified { get; set; }

    public string? Logo { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public string? Comments { get; set; }

    public string? Address { get; set; }

    public bool? IsActive { get; set; }

    public virtual Country Country { get; set; }

    public virtual ICollection<InsurancePolicy> InsurancePolicies { get; set; } = new List<InsurancePolicy>();

    public virtual ICollection<InsurancePoliciesAu> InsurancePoliciesAus { get; set; } = new List<InsurancePoliciesAu>();

    public virtual ICollection<InsuranceProviderDocument> InsuranceProviderDocuments { get; set; } = new List<InsuranceProviderDocument>();

    public virtual ICollection<InsuranceProviderDocumentsAu> InsuranceProviderDocumentsAus { get; set; } = new List<InsuranceProviderDocumentsAu>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
