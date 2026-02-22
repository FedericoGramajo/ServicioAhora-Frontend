using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Address;
using ClientLibrary.Models.Authentication;
using ClientLibrary.Models.Landing;
using ClientLibrary.Services.Contracts;
using static ClientLibrary.Helper.Constant;

namespace ClientLibrary.Services.Implementations
{
    public class AddressService(IHttpClientHelper httpClient, IApiCallHelper apiHelper) : IAddressService
    {
        

        public async Task<ServiceResponse<IEnumerable<GetAddres>>> GetAddressesAsync(string userId)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Address.Get,
                Type = Constant.ApiCallType.Get,
                Client = client
            };
            apiCall.ToString(userId);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return await apiHelper.GetServiceResponse<ServiceResponse<IEnumerable<GetAddres>>>(result);
        }

        public async Task<ServiceResponse> AddAddressAsync(CreateAddres address)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Address.Add,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = address
            };
            var result = await apiHelper.ApiCallTypeCall<CreateAddres>(apiCall);
            var response = result == null ? apiHelper.ConnectionError() : await apiHelper.GetServiceResponse<ServiceResponse>(result);
            return response ?? new ServiceResponse(false, "El servidor devolvió un error inesperado al agregar la dirección");
        }

        public async Task<ServiceResponse> UpdateAddressAsync(UpdateAddress address)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Address.Update,
                Type = Constant.ApiCallType.Update,
                Client = client,
                Model = address
            };
            var result = await apiHelper.ApiCallTypeCall<UpdateAddress>(apiCall);
            var response = result == null ? apiHelper.ConnectionError() : await apiHelper.GetServiceResponse<ServiceResponse>(result);
            return response ?? new ServiceResponse(false, "El servidor devolvió un error inesperado al actualizar la dirección");
        }

        public async Task<ServiceResponse> DeleteAddressAsync(Guid id)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Address.Delete,
                Type = ApiCallType.Delete,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            var response = result == null ? apiHelper.ConnectionError() : await apiHelper.GetServiceResponse<ServiceResponse>(result);
            return response ?? new ServiceResponse(false, "El servidor devolvió un error inesperado al eliminar la dirección");
        }
    }
}
