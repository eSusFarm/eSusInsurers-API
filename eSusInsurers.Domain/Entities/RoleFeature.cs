﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class RoleFeature : BaseAuditableEntity
{
    public int RoleId { get; set; }

    public int FeatureId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Feature Feature { get; set; }

    public virtual Role Role { get; set; }
}