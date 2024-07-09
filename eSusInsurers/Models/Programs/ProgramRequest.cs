using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;
using WMS.Models.Roles;

namespace eSusInsurers.Models.Programs
{
    public class ProgramRequest : IMapFrom<eSusInsurers.Domain.Entities.Program>
    {
        public string ProgramName { get; set; } = null!;

        public int? RegionId { get; set; }

        public int? DistrictId { get; set; }

        public int? SubCountyId { get; set; }

        public int? ParishId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProgramRequest, eSusInsurers.Domain.Entities.Program>()
                .ForMember(x => x.IsActive, opt => opt.MapFrom(c => true));
        }
    }
}
