using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace WMS.Models.Roles.UpdateRole;

public class MenuRolesPrivilegeItems : IMapFrom<MenuRolesPrivilege>
{
    public int MenuRolesPrivilegeId { get; set; }

    public int ApplicationMenuId { get; set; }

    public string? ApplicationMenuName { get; set; }

    public int? ApplicationChildMenuId { get; set; }

    public string? ApplicationChildMenuName { get; set; }

    public bool Read { get; set; }

    public bool Create { get; set; }

    public bool Update { get; set; }

    public bool Delete { get; set; }

    public List<MenuRolesFunctionalityItems> MenuRolesFunctionalities { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MenuRolesPrivilegeItems, MenuRolesPrivilege>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.MenuRolesPrivilegeId))
            .ForMember(x => x.ApplicationChildMenuId, opt => opt.MapFrom(src => src.ApplicationChildMenuId))
            .ForMember(x => x.ApplicationMenuId, opt => opt.MapFrom(src => src.ApplicationMenuId))
            .ForMember(x => x.Read, opt => opt.MapFrom(src => src.Read))
            .ForMember(x => x.Create, opt => opt.MapFrom(src => src.Create))
            .ForMember(x => x.Update, opt => opt.MapFrom(src => src.Update))
            .ForMember(x => x.Delete, opt => opt.MapFrom(src => src.Delete));

        profile.CreateMap<MenuRolesPrivilege, MenuRolesPrivilegeItems>()
            .ForMember(x => x.MenuRolesPrivilegeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.ApplicationChildMenuId, opt => opt.MapFrom(src => src.ApplicationChildMenuId))
            .ForMember(x => x.ApplicationMenuName, opt => opt.MapFrom(src => src.ApplicationMenu.ApplicationMenuName))
            .ForMember(x => x.ApplicationChildMenuName,
                opt => opt.MapFrom(src =>
                    src.ApplicationChildMenuId.HasValue
                        ? src.ApplicationChildMenu.ApplicationChildMenuName
                        : string.Empty))
            .ForMember(x => x.ApplicationMenuId, opt => opt.MapFrom(src => src.ApplicationMenuId))
            .ForMember(x => x.Read, opt => opt.MapFrom(src => src.Read))
            .ForMember(x => x.Create, opt => opt.MapFrom(src => src.Create))
            .ForMember(x => x.Update, opt => opt.MapFrom(src => src.Update))
            .ForMember(x => x.Delete, opt => opt.MapFrom(src => src.Delete));
    }
}