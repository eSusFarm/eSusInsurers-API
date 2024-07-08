using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.Countries
{
    public class DistrictModel : IMapFrom<District>
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int RegionId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<District, DistrictModel>()
                                .ForMember(d => d.DistrictId, opt => opt.MapFrom(c => c.Id))
                                 .ForMember(d => d.DistrictName, opt => opt.MapFrom(c => c.DistrictName))
                                 .ForMember(d => d.RegionId, opt => opt.MapFrom(c => c.RegionId));
        }
    }
}
