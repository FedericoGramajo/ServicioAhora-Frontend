using ClientLibrary.Models;
using ClientLibrary.Models.ServicioAhora.ServOffering;

namespace ClientLibrary.Services.Contracts
{
    public interface IServOfferingService
    {
        Task<IEnumerable<GetServiceOffering>> GetAllAsync();
        Task<GetServiceOffering> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateServiceOffering serviceOffering);
        Task<ServiceResponse> UpdateAsync(UpdateServiceOffering serviceOffering);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<IEnumerable<GetServiceOffering>> SearchAsync(string query);
        Task<IEnumerable<GetServiceOffering>> GetByProfessionalAsync(string professionalId);
        Task<IEnumerable<GetServiceOffering>> GetAdminServicesByDateAsync(DateTime startDate, DateTime endDate);
    }
}
