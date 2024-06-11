using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class UserType : BaseAuditableEntity
{

    public string UserType1 { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<UsersAu> UsersAus { get; set; } = new List<UsersAu>();
}
