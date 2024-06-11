using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace WMS.Models.Roles.UpdateRole
{
    public class MenuRoleFunctionalityApprovalProcessItems : IMapFrom<MenuRoleFunctionalityApprovalProcess>
    {
        public int MenuRoleFunctionalityApprovalProcessId { get; set; }

        public int FunctionalityApprovalProcessId { get; set; }

        public bool Enable { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MenuRoleFunctionalityApprovalProcessItems, MenuRoleFunctionalityApprovalProcess>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.MenuRoleFunctionalityApprovalProcessId))
                .ForMember(x => x.FunctionalityApprovalProcessId, opt => opt.MapFrom(src => src.FunctionalityApprovalProcessId))
                .ForMember(x => x.Enable, opt => opt.MapFrom(src => src.Enable));

            profile.CreateMap<MenuRoleFunctionalityApprovalProcess, MenuRoleFunctionalityApprovalProcessItems>()
               .ForMember(x => x.FunctionalityApprovalProcessId, opt => opt.MapFrom(src => src.Id))
               .ForMember(x => x.FunctionalityApprovalProcessId, opt => opt.MapFrom(src => src.FunctionalityApprovalProcessId))
               .ForMember(x => x.Enable, opt => opt.MapFrom(src => src.Enable));
        }
    }
}
