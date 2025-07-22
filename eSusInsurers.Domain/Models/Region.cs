using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class Region : BaseAuditableEntity
{

    public string RegionName { get; set; } = null!;

    public int CountryId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<DistrictsAu> DistrictsAus { get; set; } = new List<DistrictsAu>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<LocationsAu> LocationsAus { get; set; } = new List<LocationsAu>();

    public virtual ICollection<Program> Programs { get; set; } = new List<Program>();

    public virtual ICollection<SeasonCutOffDate> SeasonCutOffDates { get; set; } = new List<SeasonCutOffDate>();
}
