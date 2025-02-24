using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class SubFunctionality : BaseAuditableEntity
{

    public string SubFunctionality1 { get; set; } = null!;

    public int FunctionalityId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Functionality Functionality { get; set; }
}
