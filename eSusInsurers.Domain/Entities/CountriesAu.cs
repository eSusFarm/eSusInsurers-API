﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class CountriesAu : BaseAuditableEntity
{
    public DateTime HistoryCreatedDate { get; set; }

    public int CountryId { get; set; }

    public string CountryName { get; set; }

    public string CountryCode { get; set; }

    public bool? IsActive { get; set; }
}