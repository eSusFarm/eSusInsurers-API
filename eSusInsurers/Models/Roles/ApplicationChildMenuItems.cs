using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace WMS.Models.Roles
{
    [ExcludeFromCodeCoverage]
    public class ApplicationChildMenuItems : IMapFrom<ApplicationChildMenu>, IMapFrom<MenuRolesPrivilege>
    {
        public int ApplicationChildMenuId { get; set; }

        public int ApplicationMenuId { get; set; }
        
        public string ApplicationChildMenuName { get; set; } = null!;
        
        public string ApplicationChildMenuLink { get; set; } = null!;
        
        public string ApplicationChildMenuIcon { get; set; } = null!;
        
        public int Sequence { get; set; }
        
        public List<ApplicationFunctionalitiesList> ApplicationFunctionalities { get; set; } = new();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationChildMenu, ApplicationChildMenuItems>()
                   .ForMember(x => x.ApplicationChildMenuId, opt => opt.MapFrom(src => src.Id))
                   .ForMember(x => x.ApplicationMenuId, opt => opt.MapFrom(src => src.ApplicationMenuId))
                   .ForMember(x => x.ApplicationChildMenuName, opt => opt.MapFrom(src => src.ApplicationChildMenuName))
                   .ForMember(x => x.ApplicationChildMenuIcon, opt => opt.MapFrom(src => src.ApplicationChildMenuIcon))
                   .ForMember(x => x.Sequence, opt => opt.MapFrom(src => src.Sequence))
                   .ForMember(x => x.ApplicationChildMenuLink, opt => opt.MapFrom(src => src.ApplicationChildMenuLink));

            profile.CreateMap<ApplicationChildMenuItems, MenuRolesPrivilege>()
                .ForMember(x => x.ApplicationChildMenuId, opt => opt.MapFrom(src => src.ApplicationChildMenuId))
                .ForMember(x => x.ApplicationMenuId, opt => opt.MapFrom(src => src.ApplicationMenuId))
                .ForMember(x => x.Read, opt => opt.MapFrom(src => false))
                .ForMember(x => x.Create, opt => opt.MapFrom(src => false))
                .ForMember(x => x.Update, opt => opt.MapFrom(src => false))
                .ForMember(x => x.Delete, opt => opt.MapFrom(src => false))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(src => true));
        }
    }
}
