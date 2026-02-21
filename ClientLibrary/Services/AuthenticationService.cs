using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Authentication;
using ClientLibrary.Models.Category;
using System.Net.Http.Json;
using System.Web;

namespace ClientLibrary.Services
{
    public class AuthenticationService(IApiCallHelper apiHelper, IHttpClientHelper httpClient) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.Register,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = user
            };
            var result = await apiHelper.ApiCallTypeCall<CreateUser>(apiCall);
            return result == null ? apiHelper.ConnectionError() : 
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.Login,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = user
            };
            var result = await apiHelper.ApiCallTypeCall<LoginUser>(apiCall);
            return result == null ? new LoginResponse(Message: apiHelper.ConnectionError().Message) :
                await apiHelper.GetServiceResponse<LoginResponse>(result);
        }

        public async Task<LoginResponse> RequestPasswordReset(RequestPasswordReset user)
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.RequestPasswordReset,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = user
            };
            var result = await apiHelper.ApiCallTypeCall<RequestPasswordReset>(apiCall);
            return result == null ? new LoginResponse(Message: apiHelper.ConnectionError().Message) :
                await apiHelper.GetServiceResponse<LoginResponse>(result);
        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.ReviveToken,
                Type = Constant.ApiCallType.Get,
                Client = client,
                Model = null!,
                Id = HttpUtility.UrlEncode(refreshToken)
            };
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result == null ? null! : await apiHelper.GetServiceResponse<LoginResponse>(result);
        }

        public async Task<ServiceResponse> VerifyToken(VerifyToken verifyToken)
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.VerifyToken,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = verifyToken
            };

            var result = await apiHelper.ApiCallTypeCall<VerifyToken>(apiCall);
            return result == null ? apiHelper.ConnectionError() :
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }
        public async Task<ServiceResponse> ChangePassword(ChangePassword changePassword)
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.ChangePassword,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = changePassword
            };

            var result = await apiHelper.ApiCallTypeCall<ChangePassword>(apiCall);
            return result == null ? apiHelper.ConnectionError() :
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }

        public async Task<UserDto> GetMyProfile()
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.Me,
                Type = Constant.ApiCallType.Get,
                Client = client,
                Model = null!,
                Id = null!
            };
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            if (result.IsSuccessStatusCode)
                return await apiHelper.GetServiceResponse<UserDto>(result);
            else
                return null!;
            //try
            //{
            //    System.Net.Http.HttpClient privateClient = await httpClient.GetPrivateClientAsync();
            //    var response = await privateClient.GetAsync(Constant.Authentication.Me);
            //    if (!response.IsSuccessStatusCode) return new UserDto();

            //    var content = await response.Content.ReadAsStringAsync();
            //    var result = System.Text.Json.JsonSerializer.Deserialize<UserDto>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //    return result ?? new UserDto();
            //}
            //catch
            //{
            //    return new UserDto();
            //}
        }
        
        public async Task<ServiceResponse> LogoutAsync()
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.Logout,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = null!
            };
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result == null ? apiHelper.ConnectionError() :
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }
    }
}
