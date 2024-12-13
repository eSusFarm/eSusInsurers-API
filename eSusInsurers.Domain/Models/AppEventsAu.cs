using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class AppEventsAu : BaseAuditableEntity
{

    public DateTime? HistoryCreatedDate { get; set; }

    public int EventId { get; set; }

    public string? EventName { get; set; }

    public bool? IsActive { get; set; }
}
