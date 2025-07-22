using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace WMS.Models.Roles
{
    public class FunctionalityApprovalProcessList : IMapFrom<FunctionalityApprovalProcess>, IMapFrom<MenuRoleFunctionalityApprovalProcess>
    {
        public int? MenuRoleFunctionalityApprovalProcessId { get; set; }

        public int FunctionalityApprovalProcessId { get; set; }
        
        public int? ApplicationFunctionalityId { get; set; }
        
        public string ApprovalProcess { get; set; } = null!;
        
        public bool Enable { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FunctionalityApprovalProcess, FunctionalityApprovalProcessList>()
                .ForMember(dest => dest.FunctionalityApprovalProcessId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ApplicationFunctionalityId, opt => opt.MapFrom(src => src.ApplicationFunctionalityId))
                .ForMember(dest => dest.ApprovalProcess, opt => opt.MapFrom(src => src.ApprovalProcess));

            profile.CreateMap<FunctionalityApprovalProcessList, MenuRoleFunctionalityApprovalProcess>()
                .ForMember(dest => dest.FunctionalityApprovalProcessId, opt => opt.MapFrom(src => src.FunctionalityApprovalProcessId))
                .ForMember(dest => dest.Enable, opt => opt.MapFrom(src => false))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(src => true));

        }
    }
}
