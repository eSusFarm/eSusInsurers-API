using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class InsurancePolicy1 : BaseAuditableEntity
{

    public int? CompanyId { get; set; }

    public int? CategoryId { get; set; }

    public string PolicyName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual CropInsuranceCategory? Category { get; set; }

    public virtual InsuranceCompany? Company { get; set; }
}
