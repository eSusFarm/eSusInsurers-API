using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class Location : BaseAuditableEntity
{

    public string LocationName { get; set; } = null!;

    public int RegionId { get; set; }

    public int DistrictId { get; set; }

    public int? SubCountyId { get; set; }

    public string Latitude { get; set; } = null!;

    public string Longitude { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual District District { get; set; }

    public virtual ICollection<InsuranceRiskAu> InsuranceRiskAus { get; set; } = new List<InsuranceRiskAu>();

    public virtual ICollection<InsuranceRisk> InsuranceRisks { get; set; } = new List<InsuranceRisk>();

    public virtual Region Region { get; set; }

    public virtual SubCounty SubCounty { get; set; }
}
