﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class SeasonsAu : BaseAuditableEntity
{
    public DateTime HistoryCreatedDate { get; set; }

    public int SeasonId { get; set; }

    public string SeasonName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? LocationId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Location Location { get; set; }
}