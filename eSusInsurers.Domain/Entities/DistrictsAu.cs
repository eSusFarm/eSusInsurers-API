using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class DistrictsAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int DistrictId { get; set; }

    public string DistrictName { get; set; } = null!;

    public int RegionId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Region Region { get; set; }
}
