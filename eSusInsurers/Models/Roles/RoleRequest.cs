using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace WMS.Models.Roles;

public class RoleRequest : IMapFrom<Role>
{
    public string RoleName { get; set; } = null!;

    public int? ReportingToId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RoleRequest, Role>()
            .ForMember(x => x.RoleName, opt => opt.MapFrom(c => c.RoleName))
            .ForMember(x => x.ReportingToId, opt => opt.MapFrom(c => c.ReportingToId))
            .ForMember(x => x.IsActive, opt => opt.MapFrom(c => true));
    }
}