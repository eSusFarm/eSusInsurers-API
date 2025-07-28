using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class PaymentModesAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int PaymentModeId { get; set; }

    public string? PaymentMode { get; set; }

    public bool? IsActive { get; set; }
}
