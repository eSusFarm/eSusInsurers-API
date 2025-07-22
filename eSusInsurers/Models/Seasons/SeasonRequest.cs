using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;
using WMS.Models.Roles;

namespace eSusInsurers.Models.Seasons
{
    public class SeasonRequest : IMapFrom<Season>
    {
        public string SeasonName { get; set; } = null!;

        public string SeasonYear { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SeasonRequest, Season>()
                .ForMember(x => x.IsActive, opt => opt.MapFrom(c => true));
        }
    }
}
