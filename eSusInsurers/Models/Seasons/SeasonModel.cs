using System.Text.Json.Serialization;
using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.Seasons;

public class SeasonModel : IMapFrom<Season>
{
    public int SeasonId { get; set; }

    public string SeasonName { get; set; } = null!;

    public string? SeasonYear { get; set; }

    public bool IsActive { get; set; }

    [JsonIgnore] public DateTime Createddate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Season, SeasonModel>()
            .ForMember(d => d.SeasonId, opt => opt.MapFrom(c => c.Id));
    }
}