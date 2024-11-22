using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;

namespace eSusInsurers.Models.Users.UpdateUser;

public class UpdateUserProfileRequestModel : IMapFrom<User>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Gender { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserProfileRequestModel, User>();
    }
}