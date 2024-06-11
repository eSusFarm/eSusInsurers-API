using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class ApplicationSetting : BaseAuditableEntity
{

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public bool IsActive { get; set; }
}
