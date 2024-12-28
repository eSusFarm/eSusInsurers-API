namespace eSusInsurers.Models.Roles.RoleDetails;

public class MenuRoleFunctionalityApprovalProcess
{
    public int MenuRoleFunctionalityApprovalProcessId { get; set; }

    public int FunctionalityApprovalProcessId { get; set; }

    public string? ApprovalProcess { get; set; }

    public bool Enable { get; set; }
}