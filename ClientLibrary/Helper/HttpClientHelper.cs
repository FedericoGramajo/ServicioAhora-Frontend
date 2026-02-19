using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ClientLibrary.Helper
{
    public class HttpClientHelper(IHttpClientFactory clientFactory, ITokenService tokenService) : IHttpClientHelper
    {
        public async Task<HttpClient> GetPrivateClientAsync()
        {
            var client = clientFactory.CreateClient(Constant.ApiClient.Name);
            var token = await tokenService.GetJWTTokenAsync(Constant.Cookie.Name);
            if (string.IsNullOrEmpty(token))
                return client;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constant.Authentication.Type, token);
            return client;
        }

        public async Task<string?> GetUserIdAsync()
        {
            var jwt = await tokenService.GetJWTTokenAsync(Constant.Cookie.Name);
            if (string.IsNullOrEmpty(jwt)) return null;
            try
            {
                // JWT = header.payload.signature — decode payload (part[1])
                var parts = jwt.Split('.');
                if (parts.Length < 2) return null;

                var payload = parts[1];
                // Add padding if needed
                payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
                var json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                // Try 'sub' first, then common NameIdentifier variations
                foreach (var key in new[] { "sub", "nameid", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" })
                {
                    if (root.TryGetProperty(key, out var val))
                        return val.GetString();
                }
                return null;
            }
            catch { return null; }
        }

        public HttpClient GetPublicClient()
        {
            return clientFactory.CreateClient(Constant.ApiClient.Name);
        }
    }
}
