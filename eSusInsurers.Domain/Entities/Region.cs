﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class Region : BaseAuditableEntity
{
    public string RegionName { get; set; }

    public int CountryId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Country Country { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<DistrictsAu> DistrictsAus { get; set; } = new List<DistrictsAu>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<LocationsAu> LocationsAus { get; set; } = new List<LocationsAu>();
}