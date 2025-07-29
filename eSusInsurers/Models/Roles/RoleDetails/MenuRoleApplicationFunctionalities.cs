namespace eSusInsurers.Models.Roles.RoleDetails
{
    public class MenuRoleApplicationFunctionalities
    {
        public int MenuRolesFunctionalityId { get; set; }
        
        public int ApplicationFunctionalityId { get; set; }
        
        public string? Functionality { get; set; }
        
        public bool Enable { get; set; }
        
        public List<MenuRoleFunctionalityApprovalProcess> FunctionalityApprovalProcess { get; set; } = new();
    }
}
