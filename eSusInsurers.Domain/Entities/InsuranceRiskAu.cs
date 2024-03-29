﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class InsuranceRiskAu : BaseAuditableEntity
{
    public DateTime HistoryCreatedDate { get; set; }

    public int InsuranceRiskId { get; set; }

    public int InsurancePolicyId { get; set; }

    public int SeasonId { get; set; }

    public int LocationId { get; set; }

    public string CropName { get; set; }

    public decimal? Deductible { get; set; }

    public decimal? DroughtLoss { get; set; }

    public decimal? ExcessRainfallLoss { get; set; }

    public decimal? TotalLoss { get; set; }

    public decimal? Payout { get; set; }

    public decimal? FinalPayout { get; set; }

    public bool? IsActive { get; set; }

    public virtual InsurancePolicy InsurancePolicy { get; set; }

    public virtual Location Location { get; set; }

    public virtual Season Season { get; set; }
}