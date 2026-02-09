using ClientLibrary.Helper;
using Microsoft.JSInterop;

public class CookieStorageService : ICookieStorageService
{
    private readonly IJSRuntime _js;

    public CookieStorageService(IJSRuntime js)
        => _js = js;

    public async Task SetCookieAsync(string key, string value, int? days = null, string path = "/")
    {
        var expires = days.HasValue
            ? $"expires={DateTime.UtcNow.AddDays(days.Value).ToUniversalTime():R}; "
            : string.Empty;
        var cookie = $"{key}={Uri.EscapeDataString(value)}; {expires}path={path}";
        await _js.InvokeVoidAsync("eval", $"document.cookie = \"{cookie}\"");
    }

    public async Task<string?> GetCookieAsync(string key)
    {
        return await _js.InvokeAsync<string?>("getCookie", key);
    }

    public async Task RemoveCookieAsync(string key, string path = "/")
    {
        await SetCookieAsync(key, string.Empty, -1, path);
    }
}
