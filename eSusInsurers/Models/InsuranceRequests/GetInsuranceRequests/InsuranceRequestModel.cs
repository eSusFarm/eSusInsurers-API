using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.InsuranceRequests.getInsuranceRequests
{
    /// <summary>
    /// Insurance Requests Model
    /// </summary>
    public class InsuranceRequestModel: IMapFrom<InsuranceRequest>
    {
        public string FarmerName { get; set; } = null!;

        public string CropName { get; set; } = null!;

        public string FarmLocationDistrict { get; set; } = null!;

        public string? FarmLocationSubCounty { get; set; }

        public string? FarmLocationParish { get; set; }

        public string? FarmLocationWard { get; set; }

        public string? FarmLocationVillage { get; set; }

        public bool? IsFarmLocationSameAsHomeLocation { get; set; }

        public string HomeLocationDistrict { get; set; } = null!;

        public string? HomeLocationSubCounty { get; set; }

        public string? HomeLocationParish { get; set; }

        public string? HomeLocationWard { get; set; }

        public string? HomeLocationVillage { get; set; }

        public string? ProgramName { get; set; }

        public string? InsuranceCompanyName { get; set; }

        public bool? IsRequestSubmitted { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<InsuranceRequest, InsuranceRequestModel>()
                .ForMember(d => d.IsRequestSubmitted, o => o.MapFrom(s => s.IsRequestSubmitted))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive))
                .ForMember(d => d.InsuranceCompanyName, o => o.MapFrom(s => s.InsuranceCompanyName))
                .ForMember(d => d.ProgramName, o => o.MapFrom(s => s.ProgramName))
                .ForMember(d => d.HomeLocationVillage, o => o.MapFrom(s => s.HomeLocationVillage))
                .ForMember(d => d.FarmLocationDistrict, o => o.MapFrom(s => s.FarmLocationDistrict))
                .ForMember(d => d.FarmLocationVillage, o => o.MapFrom(s => s.FarmLocationVillage))
                .ForMember(d => d.FarmerName, o => o.MapFrom(s => s.FarmerName))
                .ForMember(d => d.CropName, o => o.MapFrom(s => s.CropName));
        }
    }  
    
}

