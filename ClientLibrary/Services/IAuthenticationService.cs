using ClientLibrary.Models;
using ClientLibrary.Models.Authentication;

namespace ClientLibrary.Services
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> CreateUser(CreateUser user);
        Task<LoginResponse> LoginUser(LoginUser user);
        Task<LoginResponse> ReviveToken(string refreshToken);
        Task<LoginResponse> RequestPasswordReset(RequestPasswordReset user);
        Task<ServiceResponse> VerifyToken(VerifyToken verifyToken);
        Task<ServiceResponse> ChangePassword(ChangePassword changePassword);
        Task<UserDto> GetMyProfile();
        Task<ServiceResponse> LogoutAsync();
    }
}
