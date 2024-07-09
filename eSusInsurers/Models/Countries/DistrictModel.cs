using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.Countries
{
    public class DistrictModel : IMapFrom<District>
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = null!;
        public int RegionId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<District, DistrictModel>()
                                .ForMember(d => d.DistrictId, opt => opt.MapFrom(c => c.Id));
        }
    }
}
