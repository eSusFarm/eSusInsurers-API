using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class EmailTemplate : BaseAuditableEntity
{

    public int? EventId { get; set; }

    public int? RoleId { get; set; }

    public string MailSubject { get; set; } = null!;

    public string MailContent { get; set; } = null!;

    public string MailTo { get; set; } = null!;

    public string? Cc { get; set; }

    public string? Bcc { get; set; }

    public bool? IsActive { get; set; }

    public virtual AppEvent Event { get; set; }

    public virtual Role Role { get; set; }
}
