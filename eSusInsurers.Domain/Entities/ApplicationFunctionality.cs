using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class ApplicationFunctionality : BaseAuditableEntity
{

    public string Functionality { get; set; } = null!;

    public int ApplicationMenuId { get; set; }

    public int? ApplicationChildMenuId { get; set; }

    public bool IsActive { get; set; }

    public virtual ApplicationChildMenu ApplicationChildMenu { get; set; }

    public virtual ApplicationMenu ApplicationMenu { get; set; }

    public virtual ICollection<FunctionalityApprovalProcess> FunctionalityApprovalProcesses { get; set; } = new List<FunctionalityApprovalProcess>();

    public virtual ICollection<MenuRolesFunctionality> MenuRolesFunctionalities { get; set; } = new List<MenuRolesFunctionality>();
}
