using AutoMapper;

public class ProgramsProfile : Profile
{
    public ProgramsProfile()
    {
        // Assuming ProgramsModel implements IMapFrom<Domain.Entities.Program>
        new eSusInsurers.Models.Programs.ProgramsModel().Mapping(this);
    }
}