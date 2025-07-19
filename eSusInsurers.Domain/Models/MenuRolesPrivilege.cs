using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class MenuRolesPrivilege : BaseAuditableEntity
{

    public int RoleId { get; set; }

    public int ApplicationMenuId { get; set; }

    public int? ApplicationChildMenuId { get; set; }

    public bool Read { get; set; }

    public bool Create { get; set; }

    public bool Update { get; set; }

    public bool Delete { get; set; }

    public bool IsActive { get; set; }

    public virtual ApplicationChildMenu? ApplicationChildMenu { get; set; }

    public virtual ApplicationMenu ApplicationMenu { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
