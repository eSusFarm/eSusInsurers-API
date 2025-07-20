using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;
namespace eSusInsurers.Models.Countries;

public class RegionModel : IMapFrom<Region>
{
    public int RegionId { get; set; }
    public string RegionName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Region, RegionModel>()
            .ForMember(d => d.RegionId, opt => opt.MapFrom(c => c.Id));
    }
}