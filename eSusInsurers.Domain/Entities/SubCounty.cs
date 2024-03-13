﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class SubCounty : BaseAuditableEntity
{
    public string SubCountyName { get; set; }

    public int DistrictId { get; set; }

    public bool? IsActive { get; set; }

    public virtual District District { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<LocationsAu> LocationsAus { get; set; } = new List<LocationsAu>();
}