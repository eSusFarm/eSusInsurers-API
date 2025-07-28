using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models
{
    public class CropCategoryModel : IMapFrom<CropCategory>
    {
        public int? CropCategoryId { get; set; }

        public string CropCategoryName { get; set; } = null!;

        public bool? IsActive { get; set; }

        public int? ParentCategoryId { get; set; }

        public int? SequenceId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CropCategory, CropCategoryModel>()
                                .ForMember(d => d.CropCategoryId, opt => opt.MapFrom(c => c.Id));
        }
    }
}