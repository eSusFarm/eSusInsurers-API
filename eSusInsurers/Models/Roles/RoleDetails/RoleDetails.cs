using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.Roles.RoleDetails;

public class RoleDetails : IMapFrom<SP_GetRoleDetailsResult>
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public int? ReportingToId { get; set; }

    public string? ReportRoleName { get; set; }

    public List<MenuRolePrivileges>? MenuRolePrivileges { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SP_GetRoleDetailsResult, RoleDetails>()
            .ForMember(x => x.MenuRolePrivileges, opt => opt.Ignore());
    }
}