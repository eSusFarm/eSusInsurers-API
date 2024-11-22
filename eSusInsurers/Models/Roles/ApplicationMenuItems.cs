using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace WMS.Models.Roles;

public class ApplicationMenuItems : IMapFrom<ApplicationMenu>, IMapFrom<MenuRolesPrivilege>
{
    public int ApplicationMenuId { get; set; }

    public string ApplicationMenuName { get; set; } = null!;

    public string ApplicationMenuLink { get; set; } = null!;

    public string ApplicationMenuIcon { get; set; } = null!;

    public int Sequence { get; set; }

    public List<ApplicationChildMenuItems> ApplicationChildMenuItems { get; set; } = new();

    public List<ApplicationFunctionalitiesList> ApplicationFunctionalities { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApplicationMenu, ApplicationMenuItems>()
            .ForMember(x => x.ApplicationMenuId, opt => opt.MapFrom(c => c.Id))
            .ForMember(x => x.ApplicationChildMenuItems, opt => opt.Ignore())
            .ForMember(x => x.ApplicationFunctionalities, opt => opt.Ignore());

        profile.CreateMap<ApplicationMenuItems, MenuRolesPrivilege>()
            .ForMember(x => x.ApplicationMenuId, opt => opt.MapFrom(c => c.ApplicationMenuId))
            .ForMember(x => x.Read, opt => opt.MapFrom(src => false))
            .ForMember(x => x.Create, opt => opt.MapFrom(src => false))
            .ForMember(x => x.Update, opt => opt.MapFrom(src => false))
            .ForMember(x => x.Delete, opt => opt.MapFrom(src => false))
            .ForMember(x => x.IsActive, opt => opt.MapFrom(src => true));
    }
}