using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Models.Users.GetUsers;
using System.Text.Json.Serialization;

namespace eSusInsurers.Models.Programs
{
    public class ProgramsModel : IMapFrom<eSusInsurers.Domain.Entities.Program>
    {
        public int ProgramId { get; set; }

        public string ProgramName { get; set; } = null!;

        public string RegionName { get; set; }

        public string? DistrictName { get; set; }

        public string? SubCountyName { get; set; }

        public string? ParishName { get; set; }

        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<eSusInsurers.Domain.Entities.Program, ProgramsModel>()
                                .ForMember(d => d.ProgramId, opt => opt.MapFrom(c => c.Id))
                                 .ForMember(d => d.ProgramName, opt => opt.MapFrom(c => c.ProgramName))
                                 .ForMember(d => d.RegionName, opt => opt.MapFrom(c => c.Region.RegionName))
                                 .ForMember(d => d.DistrictName, opt => opt.MapFrom(c => c.District.DistrictName))
                                 .ForMember(d => d.SubCountyName, opt => opt.MapFrom(c => c.SubCounty.SubCountyName))
                                 .ForMember(d => d.ParishName, opt => opt.MapFrom(c => c.Parish.ParishName));

        }
    }
}
