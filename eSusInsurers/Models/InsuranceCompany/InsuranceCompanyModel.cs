using AutoMapper;
using eSusInsurers.Common.Mappings;

namespace eSusInsurers.Models.InsuranceCompany
{
    public class InsuranceCompanyModel : IMapFrom<Domain.Entities.InsuranceCompany>
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.InsuranceCompany, InsuranceCompanyModel>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
