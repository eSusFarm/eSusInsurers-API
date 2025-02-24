using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.Seasons;

public class UpdateSeasonRequest : IMapFrom<Season>
{
    public string SeasonName { get; set; } = null!;

    public string SeasonYear { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateSeasonRequest, Season>()
            .ForMember(x => x.IsActive, opt => opt.MapFrom(c => true));
    }
}