using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Helpers;

namespace eSusInsurers.Models
{
    public class UserRegisterRequest : IMapFrom<User>
    {
        public string UserName { get; set; }

        public int UserTypeId { get; set; }

        public int? InsurerUserId { get; set; }

        public int RoleId { get; set; }

        public int? ReportingToId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserRegisterRequest, User>()
                .ForMember(x => x.IsEnforcePassword, opt => opt.MapFrom(c => true))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(c => true))
                .ForMember(x => x.PasswordSalt, opt => opt.MapFrom(c => PasswordHasher.GenerateSalt()));
        }
    }
}
