﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class CropsAu : BaseAuditableEntity
{
    public DateTime HistoryCreatedDate { get; set; }

    public int CropId { get; set; }

    public string CropName { get; set; }

    public int? CropCategoryId { get; set; }

    public bool? IsActive { get; set; }

    public decimal MinOrderQuantity { get; set; }

    public int QuantityUnits { get; set; }

    public int? SequenceId { get; set; }

    public virtual CropCategory CropCategory { get; set; }
}