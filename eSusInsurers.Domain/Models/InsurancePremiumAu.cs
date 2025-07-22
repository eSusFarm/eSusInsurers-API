using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class InsurancePremiumAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int InsurancePremiumId { get; set; }

    public int InsurancePolicyId { get; set; }

    public int? InsuranceRiskId { get; set; }

    public decimal? TotalPremiumAmount { get; set; }

    public decimal? PremiumAmountCurrency { get; set; }

    public decimal? SumInsuredAmount { get; set; }

    public decimal? SumInsuredAmountCurrency { get; set; }

    public bool? IsActive { get; set; }

    public virtual InsurancePolicy InsurancePolicy { get; set; } = null!;

    public virtual InsuranceRisk? InsuranceRisk { get; set; }
}
