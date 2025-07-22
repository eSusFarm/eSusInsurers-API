using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class SubCountiesAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int SubCountyId { get; set; }

    public string SubCountyName { get; set; } = null!;

    public int DistrictId { get; set; }

    public bool? IsActive { get; set; }

    public virtual District District { get; set; } = null!;
}
