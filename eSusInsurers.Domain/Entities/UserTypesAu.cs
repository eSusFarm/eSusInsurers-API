using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class UserTypesAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int TypeId { get; set; }

    public string UserType { get; set; } = null!;

    public bool? IsActive { get; set; }
}
