using ClientLibrary.Models.Authentication.Rol.Professional;
using ClientLibrary.Models;

namespace ClientLibrary.Services
{
    public interface IProfessionalService
    {
        Task<List<GetProfessional>> GetActiveProfessionalsAsync();
    }
}
