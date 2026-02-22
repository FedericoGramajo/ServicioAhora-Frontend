using ClientLibrary.Models.Authentication.Rol.Professional;
using ClientLibrary.Models;

namespace ClientLibrary.Services.Contracts
{
    public interface IProfessionalService
    {
        Task<List<GetProfessional>> GetActiveProfessionalsAsync();
        Task<GetProfessional> GetByIdAsync(string id);
    }
}
