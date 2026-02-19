using System.Text.Json;
using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Audit;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations;

public class AuditLogService(IHttpClientHelper httpClient, IApiCallHelper apiHelper) : IAuditLogService
{
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<AuditLogResult> GetAllAsync(AuditLogFilter filter)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var query  = BuildQueryString(filter);

        var apiCall = new ApiCall
        {
            Route  = Constant.AuditLog.GetAll + query,
            Type   = Constant.ApiCallType.Get,
            Client = client
        };

        var httpResponse = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);

        if (httpResponse == null || !httpResponse.IsSuccessStatusCode)
            return Empty(filter);

        var json = await httpResponse.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
            return Empty(filter);

        // Flat array: [ {...}, {...} ]
        if (json.TrimStart().StartsWith('['))
        {
            var flat = JsonSerializer.Deserialize<List<AuditLog>>(json, JsonOpts) ?? new();
            return new AuditLogResult
            {
                Items      = flat,
                TotalCount = flat.Count,
                Page       = filter.Page,
                PageSize   = filter.PageSize,
                TotalPages = 1
            };
        }

        // Paginated object: { "data": [...], "totalCount": N, ... }
        return JsonSerializer.Deserialize<AuditLogResult>(json, JsonOpts) ?? Empty(filter);
    }

    private static AuditLogResult Empty(AuditLogFilter f) =>
        new() { Items = new(), TotalCount = 0, Page = f.Page, PageSize = f.PageSize, TotalPages = 1 };

    private static string BuildQueryString(AuditLogFilter filter)
    {
        var parts = new List<string>();

        if (!string.IsNullOrWhiteSpace(filter.UserId))
            parts.Add($"userId={Uri.EscapeDataString(filter.UserId)}");
        if (!string.IsNullOrWhiteSpace(filter.Action))
            parts.Add($"action={Uri.EscapeDataString(filter.Action)}");
        if (!string.IsNullOrWhiteSpace(filter.EntityName))
            parts.Add($"entityName={Uri.EscapeDataString(filter.EntityName)}");
        if (filter.IsSuccess.HasValue)
            parts.Add($"isSuccess={filter.IsSuccess.Value.ToString().ToLower()}");
        if (filter.FromDate.HasValue)
            parts.Add($"fromDate={filter.FromDate.Value:yyyy-MM-dd}");
        if (filter.ToDate.HasValue)
            parts.Add($"toDate={filter.ToDate.Value:yyyy-MM-dd}");

        parts.Add($"page={filter.Page}");
        parts.Add($"pageSize={filter.PageSize}");

        return "?" + string.Join("&", parts);
    }
}
