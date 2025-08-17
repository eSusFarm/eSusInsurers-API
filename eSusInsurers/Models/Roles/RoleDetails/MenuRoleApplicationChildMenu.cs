using System.Diagnostics.CodeAnalysis;

namespace eSusInsurers.Models.Roles.RoleDetails
{
    [ExcludeFromCodeCoverage]
    public class MenuRoleApplicationChildMenu
    {
        public int MenuRolesPrivilegeId { get; set; }
        
        public int ApplicationChildMenuId { get; set; }
        
        public string? ApplicationChildMenuName { get; set; }
        
        public bool Read { get; set; }
        
        public bool Update { get; set; }
        
        public bool Delete { get; set; }
        
        public bool Create { get; set; }
        
        public List<MenuRoleApplicationFunctionalities> ApplicationFunctionalities { get; set; } = new();
    }
}
