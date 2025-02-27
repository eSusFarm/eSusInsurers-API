using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.InsuranceProducts;

public class InsuranceProductModel : IMapFrom<InsurancePolicy1>
{
    public int InsurancePolicyId { get; set; }
    public string CompanyName { get; set; }
    public string CategoryName { get; set; }
    public string PolicyName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<InsurancePolicy1, InsuranceProductModel>()
            .ForMember(d => d.InsurancePolicyId, opt => opt.MapFrom(c => c.Id))
            .ForMember(d => d.CompanyName, opt => opt.MapFrom(c => c.Company.CompanyName))
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(c => c.Category.CategoryName))
            .ForMember(d => d.PolicyName, opt => opt.MapFrom(c => c.PolicyName));
    }
}