﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class CropCategory : BaseAuditableEntity
{
    public string CropCategoryName { get; set; }

    public bool? IsActive { get; set; }

    public int? ParentCategoryId { get; set; }

    public int? SequenceId { get; set; }

    public virtual ICollection<Crop> Crops { get; set; } = new List<Crop>();

    public virtual ICollection<CropsAu> CropsAus { get; set; } = new List<CropsAu>();
}