using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class SubFunctionalitiesAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int SubFunctionalityId { get; set; }

    public string SubFunctionality { get; set; } = null!;

    public int FunctionalityId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Functionality Functionality { get; set; } = null!;
}
