using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class TaxComponent : BaseAuditableEntity
{

    public string? TaxComponent1 { get; set; }

    public int? Value { get; set; }

    public bool? IsActive { get; set; }
}
