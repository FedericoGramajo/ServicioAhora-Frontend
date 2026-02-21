using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Authentication.Rol.Professional;

namespace ClientLibrary.Services.Implementations
{
    public class ProfessionalService(IHttpClientHelper httpClient, IApiCallHelper apiHelper) : IProfessionalService
    {
        public async Task<List<GetProfessional>> GetActiveProfessionalsAsync()
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constant.Professional.GetAllPublic,
                Type = Constant.ApiCallType.Get,
                Client = client
            };
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            var professionals = await apiHelper.GetServiceResponse<List<GetProfessional>>(result);
            
            // Filter active and sort by rating
            return professionals?.Where(p => p.IsActive)
                               .OrderByDescending(p => p.AvgRating)
                               .ToList() ?? new List<GetProfessional>();
        }
    }
}
