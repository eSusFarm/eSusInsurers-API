﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class Country : BaseAuditableEntity
{
    public string CountryName { get; set; }

    public string CountryCode { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<InsuranceProvider> InsuranceProviders { get; set; } = new List<InsuranceProvider>();

    public virtual ICollection<InsuranceProvidersAu> InsuranceProvidersAus { get; set; } = new List<InsuranceProvidersAu>();

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    public virtual ICollection<RegionsAu> RegionsAus { get; set; } = new List<RegionsAu>();
}