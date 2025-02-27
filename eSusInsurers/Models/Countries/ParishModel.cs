using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.Countries;

public class ParishModel : IMapFrom<Parish>
{
    public int ParishId { get; set; }
    public string ParishName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Parish, ParishModel>()
            .ForMember(d => d.ParishId, opt => opt.MapFrom(c => c.Id));
    }
}