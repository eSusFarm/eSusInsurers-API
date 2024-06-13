using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;
using System.Text.Json.Serialization;

namespace eSusInsurers.Models.Users.GetUsers
{
    public class UserModel : IMapFrom<User>
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string EmailId { get; set; } = null!;

        public bool IsAgent { get; set; }

        public int? InsurerId { get; set; }

        public string? InsurerName { get; set; }

        public decimal ContactNumber { get; set; }

        public string Role { get; set; } = null!;

        public int RoleId { get; set; }

        public string? ReportingTo { get; set; }

        public int ReportingToId { get; set; }

        public DateTime? LastLoggedInDate { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public string CreatedBy { get; set; } = null!;

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public string? ModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserModel>()
                                .ForMember(d => d.UserId, opt => opt.MapFrom(c => c.Id))
                                 .ForMember(d => d.Role, opt => opt.MapFrom(c => c.Role.RoleName))
                                 .ForMember(d => d.InsurerName, opt => opt.MapFrom(c => c.Insurer.InsurerName))
                                 .ForMember(d => d.ReportingToId, opt => opt.MapFrom(c => c.ReportingTo))
                                 .ForMember(d => d.ReportingTo, opt => opt.MapFrom(c => c.ReportingTo.HasValue ? c.ReportingToNavigation.FirstName + " " + c.ReportingToNavigation.LastName : ""));
        }
    }
}
