using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class RegionsAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int RegionId { get; set; }

    public string RegionName { get; set; } = null!;

    public int CountryId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Country Country { get; set; }
}
