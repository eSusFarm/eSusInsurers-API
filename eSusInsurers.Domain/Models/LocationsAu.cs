using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class LocationsAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public int RegionId { get; set; }

    public int DistrictId { get; set; }

    public int? SubCountyId { get; set; }

    public string Latitude { get; set; } = null!;

    public string Longitude { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;

    public virtual SubCounty? SubCounty { get; set; }
}
