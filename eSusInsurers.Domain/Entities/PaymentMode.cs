using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class PaymentMode : BaseAuditableEntity
{

    public string? PaymentMode1 { get; set; }

    public bool? IsActive { get; set; }
}
