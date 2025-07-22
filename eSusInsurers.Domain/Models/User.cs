using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class User : BaseAuditableEntity
{

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string? EmailId { get; set; }

    public decimal ContactNumber { get; set; }

    public bool IsAgent { get; set; }

    public int? InsurerId { get; set; }

    public int RoleId { get; set; }

    public string PasswordSalt { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsEnforcePassword { get; set; }

    public DateTime? LastLoggedInDate { get; set; }

    public string? ProfilePicture { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public string? Otp { get; set; }

    public DateTime? OtpExipiryTime { get; set; }

    public bool IsActive { get; set; }

    public int? ReportingTo { get; set; }

    public virtual InsuranceProvider? Insurer { get; set; }

    public virtual ICollection<User> InverseReportingToNavigation { get; set; } = new List<User>();

    public virtual User? ReportingToNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;
}
