using System.Diagnostics.CodeAnalysis;

namespace eSusInsurers.Models.Users.Login
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }

        public string? RefreshToken { get; set; }
        
        public int UserId { get; set; }

        public bool IsEnforcePassword { get; set; }

        public List<RolePrivelege> RolePriveleges { get; set; } = new();

    }

    public class RolePrivelege
    {
        public long ApplicationMenuId { get; set; }

        public string ApplicationMenuName { get; set; } = null!;

        public string ApplicationMenuLink { get; set; } = null!;

        public string ApplicationMenuIcon { get; set; } = null!;

        public string? ApplicationMenuIcon2 { get; set; } = null!;

        public int ApplicationMenuSequence { get; set; }

        public List<ApplicationChildMenu> ChildMenus { get; set; } = new();

        public bool? Read { get; set; }

        public bool? Create { get; set; }

        public bool? Update { get; set; }

        public bool? Delete { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ApplicationChildMenu
    {
        public long? ApplicationChildMenuId { get; set; }
       
        public string? ApplicationChildMenuName { get; set; }
        
        public string? ApplicationChildMenuLink { get; set; }
        
        public string? ApplicationChildMenuIcon { get; set; }
        
        public string? ApplicationChildMenuIcon2 { get; set; }
        
        public int? ApplicationChildMenuSequence { get; set; }
        
        public bool? Read { get; set; }
        
        public bool? Create { get; set; }
        
        public bool? Update { get; set; }
        
        public bool? Delete { get; set; }
    }
}
