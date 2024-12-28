using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.Countries;

public class SubCountiesModel : IMapFrom<SubCounty>
{
    public int SubCountyId { get; set; }
    public string SubCountyName { get; set; }
    public int DistrictId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SubCounty, SubCountiesModel>()
            .ForMember(d => d.SubCountyId, opt => opt.MapFrom(c => c.Id));
    }
}