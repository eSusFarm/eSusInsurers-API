using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class InsurerUsersAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int InsurerUserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public decimal ContactNumber { get; set; }

    public bool IsAgent { get; set; }

    public int? InsurerId { get; set; }

    public bool? IsActive { get; set; }

    public string? EmailId { get; set; }

    public virtual InsuranceProvider Insurer { get; set; }
}
