using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.ServicioAhora.ServOffering;

namespace ClientLibrary.Services
{
    public class ServOfferingService(IHttpClientHelper httpClient, IApiCallHelper apiHelper) : IServOfferingService
    {
        public async Task<ServiceResponse> AddAsync(CreateServiceOffering serviceOffering)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.ServiceOffering.Add,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = serviceOffering
            };
            var result = await apiHelper.ApiCallTypeCall<CreateServiceOffering>(apiCall);
            return result == null ? apiHelper.ConnectionError() : await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }
        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Product.Delete,
                Type = Constant.ApiCallType.Delete,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result == null ? apiHelper.ConnectionError() : await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }
        public async Task<ServiceResponse> UpdateAsync(UpdateServiceOffering serviceOffering)
        {

            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.ServiceOffering.Update,
                Type = Constant.ApiCallType.Update,
                Client = client,
                Id = null!,
                Model = serviceOffering
            };
            var result = await apiHelper.ApiCallTypeCall<UpdateServiceOffering>(apiCall);
            return result == null ? apiHelper.ConnectionError() : await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }

        public async Task<IEnumerable<GetServiceOffering>> GetAllAsync()
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constant.ServiceOffering.GetAll,
                Type = Constant.ApiCallType.Get,
                Client = client,
                Model = null!,
                Id = null!
            };
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);

            if (result.IsSuccessStatusCode)
                return await apiHelper.GetServiceResponse<IEnumerable<GetServiceOffering>>(result);
            else
                return [];
        }

        public async Task<GetServiceOffering> GetByIdAsync(Guid id)
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constant.Product.Get,
                Type = Constant.ApiCallType.Get,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);

            if (result.IsSuccessStatusCode)
                return await apiHelper.GetServiceResponse<GetServiceOffering>(result);
            else
                return null!;
        }
    }
}
