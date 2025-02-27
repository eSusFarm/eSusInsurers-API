using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models;

public class CropModel : IMapFrom<Crop>
{
    public int? CropId { get; set; }

    public string CropName { get; set; } = null!;

    public int? CropCategoryId { get; set; }

    public bool? IsActive { get; set; }

    public decimal MinOrderQuantity { get; set; }

    public int QuantityUnits { get; set; }

    public int? SequenceId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Crop, CropModel>()
            .ForMember(d => d.CropId, opt => opt.MapFrom(c => c.Id));
    }
}