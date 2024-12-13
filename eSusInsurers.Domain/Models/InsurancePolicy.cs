using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class InsurancePolicy : BaseAuditableEntity
{

    public string PolicyNumber { get; set; } = null!;

    public string PolicyName { get; set; } = null!;

    public int InsuranceProviderId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<CropInsuranceAu> CropInsuranceAus { get; set; } = new List<CropInsuranceAu>();

    public virtual ICollection<CropInsurance> CropInsurances { get; set; } = new List<CropInsurance>();

    public virtual ICollection<InsurancePremium> InsurancePremia { get; set; } = new List<InsurancePremium>();

    public virtual ICollection<InsurancePremiumAu> InsurancePremiumAus { get; set; } = new List<InsurancePremiumAu>();

    public virtual InsuranceProvider InsuranceProvider { get; set; } = null!;

    public virtual ICollection<InsuranceRiskAu> InsuranceRiskAus { get; set; } = new List<InsuranceRiskAu>();

    public virtual ICollection<InsuranceRisk> InsuranceRisks { get; set; } = new List<InsuranceRisk>();
}
