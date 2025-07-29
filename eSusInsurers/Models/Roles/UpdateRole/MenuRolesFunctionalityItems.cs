using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace WMS.Models.Roles.UpdateRole
{
    public class MenuRolesFunctionalityItems : IMapFrom<MenuRolesFunctionality>
    {
        public int MenuRolesFunctionalityId { get; set; }

        public int ApplicationFunctionalityId { get; set; }
        
        public string? Functionality { get; set; }
        
        public bool Enable { get; set; }
        
        public List<MenuRoleFunctionalityApprovalProcessItems> MenuRoleFunctionalityApprovalProcess { get; set; } = new();
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MenuRolesFunctionalityItems, MenuRolesFunctionality>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.MenuRolesFunctionalityId))
                .ForMember(x => x.ApplicationFunctionalityId, opt => opt.MapFrom(src => src.ApplicationFunctionalityId))
                .ForMember(x => x.Enable, opt => opt.MapFrom(src => src.Enable));

            profile.CreateMap<MenuRolesFunctionality, MenuRolesFunctionalityItems>()
                .ForMember(x => x.MenuRolesFunctionalityId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Functionality, opt => opt.MapFrom(src => src.ApplicationFunctionality.Functionality))
                .ForMember(x => x.ApplicationFunctionalityId, opt => opt.MapFrom(src => src.ApplicationFunctionalityId))
                .ForMember(x => x.Enable, opt => opt.MapFrom(src => src.Enable));
        }
    }
}
