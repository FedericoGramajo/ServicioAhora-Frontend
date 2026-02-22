using ClientLibrary.Models.Landing;
using ClientLibrary.Models;
using ClientLibrary.Models.Address;

namespace ClientLibrary.Services.Contracts
{
    public interface IAddressService
    {
        Task<ServiceResponse<IEnumerable<GetAddres>>> GetAddressesAsync(string userId);
        Task<ServiceResponse> AddAddressAsync(CreateAddres address);
        Task<ServiceResponse> UpdateAddressAsync(UpdateAddress address);
        Task<ServiceResponse> DeleteAddressAsync(Guid id);
    }
}
