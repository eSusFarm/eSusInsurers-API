namespace eSusInsurers.Models.Users.GetUsers;

public class UserFilterOptions
{
    /// <summary>
    ///     Filter based on active users.
    /// </summary>
    public bool? IsActive { get; set; }

    public string? UserNameOrEmailId { get; set; }

    public string? Role { get; set; }
}