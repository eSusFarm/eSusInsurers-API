using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class InsuranceCompanyAu : BaseAuditableEntity
{

    public int? CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public bool? IsActive { get; set; }

    public string? ActionType { get; set; }

    public DateTime? ActionTimestamp { get; set; }
}
