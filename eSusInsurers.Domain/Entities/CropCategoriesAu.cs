﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class CropCategoriesAu : BaseAuditableEntity
{
    public DateTime HistoryCreatedDate { get; set; }

    public int CropCategoryId { get; set; }

    public string CropCategoryName { get; set; }

    public bool? IsActive { get; set; }

    public int? ParentCategoryId { get; set; }

    public int? SequenceId { get; set; }
}