using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace WMS.Models.Roles.UpdateRole;

public class UpdateRoleRequestModel : IMapFrom<Role>
{
    public string RoleName { get; set; } = null!;

    public int? ReportingToId { get; set; }

    public List<MenuRolesPrivilegeItems> MenuRolesPrivileges { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateRoleRequestModel, Role>()
            .ForMember(x => x.RoleName, opt => opt.MapFrom(c => c.RoleName))
            .ForMember(x => x.ReportingToId, opt => opt.MapFrom(c => c.ReportingToId))
            .ForMember(x => x.IsActive, opt => opt.MapFrom(c => true))
            .ForMember(x => x.MenuRolesPrivileges, opt => opt.Ignore());
    }
}