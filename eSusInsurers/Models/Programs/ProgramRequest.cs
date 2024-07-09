using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;
using WMS.Models.Roles;

namespace eSusInsurers.Models.Programs
{
    public class ProgramRequest : IMapFrom<eSusInsurers.Domain.Entities.Program>
    {
        public string ProgramName { get; set; } = null!;

        public string InstitutionName { get; set; } = null!;

        public int? RegionId { get; set; }

        public int? DistrictId { get; set; }

        public int? SubCountyId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProgramRequest, eSusInsurers.Domain.Entities.Program>()
                .ForMember(x => x.ProgramName, opt => opt.MapFrom(c => c.ProgramName))
                .ForMember(x => x.InstitutionName, opt => opt.MapFrom(c => c.InstitutionName))
                .ForMember(x => x.RegionId, opt => opt.MapFrom(c => c.RegionId))
                .ForMember(x => x.DistrictId, opt => opt.MapFrom(c => c.DistrictId))
                .ForMember(x => x.SubCountyId, opt => opt.MapFrom(c => c.SubCountyId))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(c => true));
        }
    }
}
