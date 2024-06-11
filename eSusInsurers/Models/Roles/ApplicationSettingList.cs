using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace WMS.ModelLibrary.Common.Roles
{
    public class ApplicationSettingList : IMapFrom<ApplicationSetting>
    {
        public int ApplicationSettingId { get; set; }

        public string Key { get; set; } = null!;
        
        public string Value { get; set; } = null!;
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationSetting, ApplicationSettingList>()
                .ForMember(dest => dest.ApplicationSettingId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
