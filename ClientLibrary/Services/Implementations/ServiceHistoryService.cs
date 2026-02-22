using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Landing;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations
{
    public class ServiceHistoryService(IHttpClientHelper httpClient, IApiCallHelper apiHelper) : IServiceHistoryService
    {
        public async Task<List<ServiceHistoryItem>> GetHistoryAsync()
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.ServiceHistory.GetHistory,
                Type = Constant.ApiCallType.Get,
                Client = client
            };
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            
            // Should return just the list if the API returns List directly, 
            // but apiHelper.GetServiceResponse usually expects ServiceResponse<T> structure.
            // Assuming the standard ServiceResponse wrapper:
            return await apiHelper.GetServiceResponse<List<ServiceHistoryItem>>(result);
        }
    }
}
