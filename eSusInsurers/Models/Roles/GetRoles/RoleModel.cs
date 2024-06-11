using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.Roles.GetRoles
{
    public class RoleModel : IMapFrom<Role>
    {
        public string Role { get; set; } = null!;

        public string RoleShortCode { get; set; } = null!;

        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        public int? ReportingToId { get; set; }

        public string ReportingTo { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, RoleModel>()
                                .ForMember(d => d.RoleId, opt => opt.MapFrom(c => c.Id))
                                .ForMember(d => d.Role, opt => opt.MapFrom(c => c.RoleName))
                                .ForMember(d => d.ReportingTo, opt => opt.MapFrom(c => c.ReportingTo.RoleName))
                                .ForMember(d => d.RoleShortCode, opt => opt.MapFrom(c => c.RoleName.Split()[0][0] + (c.RoleName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length > 1 ? c.RoleName.Split()[1][0].ToString() : string.Empty)));
        }
    }
}
