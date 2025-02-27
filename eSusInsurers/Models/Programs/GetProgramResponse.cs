using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.Programs;

public class GetProgramResponse
{
    public PagedResult<ProgramsModel> Programs { get; set; } = PagedResult<ProgramsModel>.Empty;
}