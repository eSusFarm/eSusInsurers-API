using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class InsurancePoliciesAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int InsurancePolicyId { get; set; }

    public string PolicyNumber { get; set; } = null!;

    public string PolicyName { get; set; } = null!;

    public int InsuranceProviderId { get; set; }

    public bool? IsActive { get; set; }

    public virtual InsuranceProvider InsuranceProvider { get; set; }
}
