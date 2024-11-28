using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class SubCounty : BaseAuditableEntity
{

    public string SubCountyName { get; set; } = null!;

    public int DistrictId { get; set; }

    public bool? IsActive { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<LocationsAu> LocationsAus { get; set; } = new List<LocationsAu>();

    public virtual ICollection<Parish> Parishes { get; set; } = new List<Parish>();

    public virtual ICollection<Program> Programs { get; set; } = new List<Program>();
}
