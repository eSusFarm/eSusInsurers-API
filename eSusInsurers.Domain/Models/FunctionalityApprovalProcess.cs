using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class FunctionalityApprovalProcess : BaseAuditableEntity
{

    public int ApplicationFunctionalityId { get; set; }

    public string ApprovalProcess { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ApplicationFunctionality ApplicationFunctionality { get; set; } = null!;

    public virtual ICollection<MenuRoleFunctionalityApprovalProcess> MenuRoleFunctionalityApprovalProcesses { get; set; } = new List<MenuRoleFunctionalityApprovalProcess>();
}
