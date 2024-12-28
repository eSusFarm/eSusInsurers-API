namespace eSusInsurers.Models.Roles.GetRoles;

/// <summary>
///     Filter options for roles.
/// </summary>
public class RoleFilterOptions
{
    /// <summary>
    ///     Filter based on active roles.
    /// </summary>
    public bool? IsActive { get; set; }
}