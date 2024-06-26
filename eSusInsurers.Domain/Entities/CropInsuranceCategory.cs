using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class CropInsuranceCategory : BaseAuditableEntity
{

    public string CategoryName { get; set; } = null!;

    public int? CompanyId { get; set; }

    public bool? IsActive { get; set; }

    public virtual InsuranceCompany Company { get; set; }

    public virtual ICollection<InsurancePolicy1> InsurancePolicy1s { get; set; } = new List<InsurancePolicy1>();
}
