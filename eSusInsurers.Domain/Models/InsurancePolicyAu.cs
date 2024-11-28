using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class InsurancePolicyAu : BaseAuditableEntity
{

    public int? InsurancePolicyId { get; set; }

    public int? CompanyId { get; set; }

    public int? CategoryId { get; set; }

    public string? PolicyName { get; set; }

    public bool? IsActive { get; set; }

    public string? ActionType { get; set; }

    public DateTime? ActionTimestamp { get; set; }
}
