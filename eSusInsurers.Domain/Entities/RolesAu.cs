using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class RolesAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public bool? IsActive { get; set; }
}
