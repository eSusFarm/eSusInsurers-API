﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class SubFunctionalitiesAu : BaseAuditableEntity
{
    public DateTime HistoryCreatedDate { get; set; }

    public int SubFunctionalityId { get; set; }

    public string SubFunctionality { get; set; }

    public int FunctionalityId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Functionality Functionality { get; set; }
}