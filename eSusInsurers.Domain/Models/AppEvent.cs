using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class AppEvent : BaseAuditableEntity
{

    public string? EventName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<EmailTemplate> EmailTemplates { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<EmailTemplatesAu> EmailTemplatesAus { get; set; } = new List<EmailTemplatesAu>();
}
