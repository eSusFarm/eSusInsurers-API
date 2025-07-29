using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class TaxComponentsAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int TaxComponentId { get; set; }

    public string? TaxComponent { get; set; }

    public int? Value { get; set; }

    public bool? IsActive { get; set; }
}
