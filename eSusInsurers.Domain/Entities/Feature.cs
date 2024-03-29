﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class Feature : BaseAuditableEntity
{
    public string Feature1 { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Functionality> Functionalities { get; set; } = new List<Functionality>();

    public virtual ICollection<FunctionalitiesAu> FunctionalitiesAus { get; set; } = new List<FunctionalitiesAu>();

    public virtual ICollection<RoleFeature> RoleFeatures { get; set; } = new List<RoleFeature>();

    public virtual ICollection<RoleFeaturesAu> RoleFeaturesAus { get; set; } = new List<RoleFeaturesAu>();
}