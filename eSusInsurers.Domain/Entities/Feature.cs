using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class Feature : BaseAuditableEntity
{

    public string Feature1 { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<Functionality> Functionalities { get; set; } = new List<Functionality>();

    public virtual ICollection<FunctionalitiesAu> FunctionalitiesAus { get; set; } = new List<FunctionalitiesAu>();
}
