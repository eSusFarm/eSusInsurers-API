using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace WMS.Models.Roles
{
    public class ApplicationFunctionalitiesList : IMapFrom<ApplicationFunctionality>, IMapFrom<MenuRolesFunctionality>
    {
        public int? MenuRolesFunctionalityId { get; set; }
        
        public int ApplicationFunctionalityId { get; set; }
        
        public int ApplicationMenuId { get; set; }
        
        public int? ApplicationChildMenuId { get; set; }
        
        public string Functionality { get; set; } = null!;
        
        public bool Enable { get; set; }
        
        public List<FunctionalityApprovalProcessList> FunctionalityApprovalProcessList { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationFunctionality, ApplicationFunctionalitiesList>()
                .ForMember(dest => dest.ApplicationFunctionalityId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ApplicationMenuId, opt => opt.MapFrom(src => src.ApplicationMenuId))
                .ForMember(dest => dest.Functionality, opt => opt.MapFrom(src => src.Functionality))
                .ForMember(dest => dest.ApplicationChildMenuId, opt => opt.MapFrom(src => src.ApplicationChildMenuId))
                .ForMember(dest => dest.Enable, opt => opt.MapFrom(src => false));

            profile.CreateMap<ApplicationFunctionalitiesList,MenuRolesFunctionality>()
                .ForMember(dest => dest.ApplicationFunctionalityId, opt => opt.MapFrom(src => src.ApplicationFunctionalityId))
                .ForMember(dest => dest.Enable, opt => opt.MapFrom(src => false))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(src => true));
        }
    }
}
