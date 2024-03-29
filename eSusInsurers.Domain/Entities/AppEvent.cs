﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class AppEvent : BaseAuditableEntity
{
    public string EventName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<EmailTemplate> EmailTemplates { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<EmailTemplatesAu> EmailTemplatesAus { get; set; } = new List<EmailTemplatesAu>();
}