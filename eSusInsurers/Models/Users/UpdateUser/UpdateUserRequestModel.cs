using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.Users.UpdateUser;

public class UpdateUserRequestModel : IMapFrom<User>
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
        profile.CreateMap<UpdateUserRequestModel, User>();
    }
}