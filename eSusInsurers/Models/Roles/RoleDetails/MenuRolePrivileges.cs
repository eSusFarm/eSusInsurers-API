namespace eSusInsurers.Models.Roles.RoleDetails
{
    public class MenuRolePrivileges
    {
        public int MenuRolesPrivilegeId { get; set; }
        
        public int ApplicationMenuId { get; set; }
        
        public string? ApplicationMenuName { get; set; }
        
        public bool Read { get; set; }
        
        public bool Update { get; set; }
       
        public bool Delete { get; set; }
       
        public bool Create { get; set; }
       
        public List<MenuRoleApplicationFunctionalities> ApplicationFunctionalities { get; set; } = new();
       
        public List<MenuRoleApplicationChildMenu> ApplicationChildMenu { get; set; } = new();
    }
}
