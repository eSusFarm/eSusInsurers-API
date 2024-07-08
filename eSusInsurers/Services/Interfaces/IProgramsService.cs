using eSusInsurers.Models.Programs;
using eSusInsurers.Models.Users.GetUsers;
using eSusInsurers.Models.Users.UpdateUser;
using WMS.Models.Roles;

namespace eSusInsurers.Services.Interfaces
{
    public interface IProgramsService
    {
        Task<Models.Common.PagedResult<ProgramsModel>> GetPrograms(GetProgramsQuery request, CancellationToken cancellationToken);
        Task<bool> AddProgram(ProgramRequest request, CancellationToken cancellationToken);
        Task UpdateProgram (int program_Id, ProgramRequest request, CancellationToken cancellationToken);
        Task<object> DeleteProgram(int program_Id, CancellationToken cancellationToken);
        Task<object> ActivateProgram(int program_Id, CancellationToken cancellationToken);
    }
}
