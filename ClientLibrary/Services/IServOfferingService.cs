using ClientLibrary.Models;
using ClientLibrary.Models.ServicioAhora.ServOffering;

namespace ClientLibrary.Services
{
    public interface IServOfferingService
    {
        Task<IEnumerable<GetServiceOffering>> GetAllAsync();
        Task<GetServiceOffering> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateServiceOffering serviceOffering);
        Task<ServiceResponse> UpdateAsync(UpdateServiceOffering serviceOffering);
        Task<ServiceResponse> DeleteAsync(Guid id);
    }
}
