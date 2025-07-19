using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class MenuRolesFunctionality : BaseAuditableEntity
{

    public int RoleId { get; set; }

    public int ApplicationFunctionalityId { get; set; }

    public bool Enable { get; set; }

    public bool IsActive { get; set; }

    public virtual ApplicationFunctionality ApplicationFunctionality { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
