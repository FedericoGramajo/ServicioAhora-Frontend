using System.Threading.Tasks;

namespace ClientLibrary.Helper
{
    public class TokenService(ICookieStorageService cookieService) : ITokenService
    {
        public string FormToken(string jwt, string refresh)
        {
            return $"{jwt}--{refresh}";
        }

        public async Task<string> GetJWTTokenAsync(string key)
        {
            return await GetTokenAsync(key, 0);
        }
        public async Task<string> GetTokenAsync(string key, int position)
        {
            try
            {
                string? token = await cookieService.GetCookieAsync(key);
                return token != null ? token.Split("--")[position] : null!;
            }
            catch
            {
                return null!;
            }
        }
        public async Task<string> GetRefreshTokenAsync(string key)
        {
            return await GetTokenAsync(key, 1);
        }

        public async Task RemoveCookie(string key)
        {
            await cookieService.RemoveCookieAsync(key);
        }

        public async Task SetCookie(string key, string value, int days, string path)
        {
            await cookieService.SetCookieAsync(key, value, days, path); 
        }
    }
}
