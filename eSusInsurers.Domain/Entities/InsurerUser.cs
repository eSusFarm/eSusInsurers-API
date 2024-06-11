using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class InsurerUser : BaseAuditableEntity
{

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public decimal ContactNumber { get; set; }

    public bool IsAgent { get; set; }

    public int? InsurerId { get; set; }

    public bool? IsActive { get; set; }

    public string? EmailId { get; set; }

    public virtual InsuranceProvider Insurer { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<UsersAu> UsersAus { get; set; } = new List<UsersAu>();
}
