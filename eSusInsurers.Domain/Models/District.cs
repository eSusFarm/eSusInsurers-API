using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class District : BaseAuditableEntity
{

    public string DistrictName { get; set; } = null!;

    public int RegionId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<LocationsAu> LocationsAus { get; set; } = new List<LocationsAu>();

    public virtual ICollection<Program> Programs { get; set; } = new List<Program>();

    public virtual Region Region { get; set; } = null!;

    public virtual ICollection<SubCounty> SubCounties { get; set; } = new List<SubCounty>();

    public virtual ICollection<SubCountiesAu> SubCountiesAus { get; set; } = new List<SubCountiesAu>();
}
