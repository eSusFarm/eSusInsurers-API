using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class MenuRoleFunctionalityApprovalProcess : BaseAuditableEntity
{

    public int? RoleId { get; set; }

    public int FunctionalityApprovalProcessId { get; set; }

    public bool Enable { get; set; }

    public bool IsActive { get; set; }

    public virtual FunctionalityApprovalProcess FunctionalityApprovalProcess { get; set; } = null!;

    public virtual Role? Role { get; set; }
}
