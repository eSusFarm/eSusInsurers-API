using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class Role : BaseAuditableEntity
{

    public string RoleName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int? ReportingToId { get; set; }

    public virtual ICollection<EmailTemplate> EmailTemplates { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<EmailTemplatesAu> EmailTemplatesAus { get; set; } = new List<EmailTemplatesAu>();

    public virtual ICollection<Role> InverseReportingTo { get; set; } = new List<Role>();

    public virtual ICollection<MenuRoleFunctionalityApprovalProcess> MenuRoleFunctionalityApprovalProcesses { get; set; } = new List<MenuRoleFunctionalityApprovalProcess>();

    public virtual ICollection<MenuRolesFunctionality> MenuRolesFunctionalities { get; set; } = new List<MenuRolesFunctionality>();

    public virtual ICollection<MenuRolesPrivilege> MenuRolesPrivileges { get; set; } = new List<MenuRolesPrivilege>();

    public virtual Role ReportingTo { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
