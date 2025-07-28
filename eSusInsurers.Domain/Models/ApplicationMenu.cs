using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class ApplicationMenu : BaseAuditableEntity
{

    public string ApplicationMenuName { get; set; } = null!;

    public string ApplicationMenuLink { get; set; } = null!;

    public string ApplicationMenuIcon { get; set; } = null!;

    public bool IsActive { get; set; }

    public int Sequence { get; set; }

    public string? ApplicationMenuIcon2 { get; set; }

    public virtual ICollection<ApplicationChildMenu> ApplicationChildMenus { get; set; } = new List<ApplicationChildMenu>();

    public virtual ICollection<ApplicationFunctionality> ApplicationFunctionalities { get; set; } = new List<ApplicationFunctionality>();

    public virtual ICollection<MenuRolesPrivilege> MenuRolesPrivileges { get; set; } = new List<MenuRolesPrivilege>();
}
