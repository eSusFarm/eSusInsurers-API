﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class PremiumPayment : BaseAuditableEntity
{
    public int? CropInsuranceId { get; set; }

    public int? CropInsurncePremiumId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? PaidAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? TotalPaidAmount { get; set; }

    public string Currency { get; set; }

    public string ModeOfPayment { get; set; }

    public string Status { get; set; }

    public bool? IsActive { get; set; }

    public virtual CropInsurance CropInsurance { get; set; }

    public virtual CropInsurancePremium CropInsurncePremium { get; set; }
}