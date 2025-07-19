using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class InsuranceCompany : BaseAuditableEntity
{

    public string CompanyName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<CropInsuranceCategory> CropInsuranceCategories { get; set; } = new List<CropInsuranceCategory>();

    public virtual ICollection<InsurancePolicy1> InsurancePolicy1s { get; set; } = new List<InsurancePolicy1>();
}
