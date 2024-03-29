﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class Season : BaseAuditableEntity
{
    public string SeasonName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? LocationId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Location Location { get; set; }

    public virtual ICollection<InsuranceRiskAu> InsuranceRiskAus { get; set; } = new List<InsuranceRiskAu>();

    public virtual ICollection<InsuranceRisk> InsuranceRisks { get; set; } = new List<InsuranceRisk>();

    public virtual ICollection<SeasonPhase> SeasonPhases { get; set; } = new List<SeasonPhase>();

    public virtual ICollection<SeasonPhasesAu> SeasonPhasesAus { get; set; } = new List<SeasonPhasesAu>();
}