using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace eSusInsurers.Domain.Entities;

[ExcludeFromCodeCoverage]
public partial class ApplicationChildMenu : BaseAuditableEntity
{

    public int ApplicationMenuId { get; set; }

    public string ApplicationChildMenuName { get; set; } = null!;

    public string ApplicationChildMenuLink { get; set; } = null!;

    public string ApplicationChildMenuIcon { get; set; } = null!;

    public bool IsActive { get; set; }

    public int Sequence { get; set; }

    public string? ApplicationChildMenuIcon2 { get; set; }

    public virtual ICollection<ApplicationFunctionality> ApplicationFunctionalities { get; set; } = new List<ApplicationFunctionality>();

    public virtual ApplicationMenu ApplicationMenu { get; set; }

    public virtual ICollection<MenuRolesPrivilege> MenuRolesPrivileges { get; set; } = new List<MenuRolesPrivilege>();
}
