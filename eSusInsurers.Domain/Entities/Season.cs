using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class Season : BaseAuditableEntity
{

    public string SeasonName { get; set; } = null!;

    public string? SeasonYear { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<SeasonCutOffDate> SeasonCutOffDates { get; set; } = new List<SeasonCutOffDate>();
}
