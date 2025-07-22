using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;
using System.Text.Json.Serialization;

namespace eSusInsurers.Models.SeasonCutOffDate
{
    public class SeasonCutOffDatesModel : IMapFrom<Domain.Entities.SeasonCutOffDate>
    {
        public int SeasonCutOffDateId { get; set; }

        public int SeasonId { get; set; }

        public string SeasonName { get; set; } = null!;

        public string? SeasonYear { get; set; }

        public int RegionId { get; set; }

        public string RegionName { get; set; } = null!;

        public int? CropCategoryId { get; set; }

        public string CropCategoryName { get; set; } = null!;

        public int? CropId { get; set; }

        public string CropName { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public DateTime Createddate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.SeasonCutOffDate, SeasonCutOffDatesModel>()
                                .ForMember(d => d.SeasonCutOffDateId, opt => opt.MapFrom(c => c.Id))
                                .ForMember(d => d.SeasonName, opt => opt.MapFrom(c => c.Season.SeasonName))
                                .ForMember(d => d.SeasonYear, opt => opt.MapFrom(c => c.Season.SeasonYear))
                                .ForMember(d => d.RegionName, opt => opt.MapFrom(c => c.Region.RegionName))
                                  .ForMember(d => d.CropName, opt => opt.MapFrom(c => c.Crop.CropName))
                                  .ForMember(d => d.CropCategoryName, opt => opt.MapFrom(c => c.CropCategory.CropCategoryName));
        }
    }
}
