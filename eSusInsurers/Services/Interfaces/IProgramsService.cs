using eSusInsurers.Models.Programs;

namespace eSusInsurers.Services.Interfaces
{
    public interface IProgramsService
    {
        Task<Models.Common.PagedResult<ProgramsModel>> GetPrograms(GetProgramsQuery request, CancellationToken cancellationToken);
        Task<bool> AddProgram(ProgramRequest request, CancellationToken cancellationToken);
        Task UpdateProgram (int programId, ProgramRequest request, CancellationToken cancellationToken);
        Task DeleteProgram(int programId, CancellationToken cancellationToken);
        Task ActivateProgram(int programId, CancellationToken cancellationToken);
    }
}
