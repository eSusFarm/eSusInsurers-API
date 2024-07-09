using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class Parish : BaseAuditableEntity
{

    public string ParishName { get; set; } = null!;

    public int SubCountyId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Program> Programs { get; set; } = new List<Program>();

    public virtual SubCounty SubCounty { get; set; }
}
