using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class PremiumPaymentsAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int PremiumPaymentId { get; set; }

    public int? CropInsuranceId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? PaidAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? TotalPaidAmount { get; set; }

    public string? Currency { get; set; }

    public string? ModeOfPayment { get; set; }

    public string? Status { get; set; }

    public bool? IsActive { get; set; }

    public virtual CropInsurance? CropInsurance { get; set; }
}
