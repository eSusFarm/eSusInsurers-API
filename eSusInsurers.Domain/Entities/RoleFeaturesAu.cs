﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class RoleFeaturesAu : BaseAuditableEntity
{
    public DateTime HistoryCreatedDate { get; set; }

    public int RoleFeatureId { get; set; }

    public int RoleId { get; set; }

    public int FeatureId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Feature Feature { get; set; }

    public virtual Role Role { get; set; }
}