using ClientLibrary.Models;
using System.Net.Http.Json;
using System.Text.Json;
namespace ClientLibrary.Helper
{
    public class ApiCallHelper : IApiCallHelper
    {
        public async Task<HttpResponseMessage> ApiCallTypeCall<TModel>(ApiCall apiCall)
        {
            try
            {
                switch (apiCall.Type)
                {
                    case "post":
                        return await apiCall.Client!.PostAsJsonAsync(apiCall.Route, (TModel)apiCall.Model!);
                    case "put":
                        return await apiCall.Client!.PutAsJsonAsync(apiCall.Route, (TModel)apiCall.Model!);
                    case "delete":
                        return await apiCall.Client!.DeleteAsync($"{apiCall.Route}/{apiCall.Id}");
                    case "get":
                        string idRoute = apiCall.Id != null ? $"/{apiCall.Id}" : null!;
                        return await apiCall.Client!.GetAsync($"{apiCall.Route}{idRoute}");
                    default:
                        throw new Exception("API call type not specified");
                }
            }
            catch { return null!; }
        }

        public ServiceResponse ConnectionError()
        {
            return new ServiceResponse(Message: "Ocurrió un error al comunicarse con el servidor");
        }

        public async Task<TResponse> GetServiceResponse<TResponse>(HttpResponseMessage message)
        {
            try
            {
                var response = await message.Content.ReadFromJsonAsync<TResponse>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                return response!;
            }
            catch
            {
                // If deserialization fails but request was successful (e.g. 204 No Content or empty 200)
                if (message.IsSuccessStatusCode && typeof(TResponse) == typeof(ServiceResponse))
                {
                    return (TResponse)(object)new ServiceResponse(true, "Operación completada exitosamente");
                }
                return default!;
            }
        }
    }
}
