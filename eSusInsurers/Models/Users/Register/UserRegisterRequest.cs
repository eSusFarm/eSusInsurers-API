using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Helpers;

namespace eSusInsurers.Models
{
    /// <summary>
    /// User Register Request
    /// </summary>
    public class UserRegisterRequest : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string EmailId { get; set; }

        public decimal ContactNumber { get; set; }

        public int? InsurerId { get; set; }

        public int RoleId { get; set; }

        public int? ReportingTo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserRegisterRequest, User>()
                .ForMember(x => x.IsEnforcePassword, opt => opt.MapFrom(c => true))
                .ForMember(x => x.IsAgent, opt => opt.MapFrom(c => false))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(c => true))
                .ForMember(x => x.PasswordSalt, opt => opt.MapFrom(c => PasswordHasher.GenerateSalt()));
        }
    }
}
