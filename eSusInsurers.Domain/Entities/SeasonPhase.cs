using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class SeasonPhase : BaseAuditableEntity
{

    public string PhaseName { get; set; } = null!;

    public int DurationInDays { get; set; }

    public int? SeasonId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Season Season { get; set; }
}
