using AutoMapper;
using eSusInsurers.Common.Mappings;

namespace eSusInsurers.Models.SeasonCutOffDate
{
    public class SeasonCutOffDatesRequest : IMapFrom<Domain.Entities.SeasonCutOffDate>
    {
        public int SeasonId { get; set; }

        public int RegionId { get; set; }

        public int? CropCategoryId { get; set; }

        public int? CropId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SeasonCutOffDatesRequest, Domain.Entities.SeasonCutOffDate>()
                .ForMember(x => x.IsActive, opt => opt.MapFrom(c => true));
        }
    }
}
