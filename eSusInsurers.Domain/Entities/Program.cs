using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class Program : BaseAuditableEntity
{

    public string ProgramName { get; set; } = null!;

    public string InstitutionName { get; set; } = null!;

    public int RegionId { get; set; }

    public int? DistrictId { get; set; }

    public int? SubCountyId { get; set; }

    public bool IsActive { get; set; }

    public virtual District District { get; set; }

    public virtual Region Region { get; set; }

    public virtual SubCounty SubCounty { get; set; }
}
