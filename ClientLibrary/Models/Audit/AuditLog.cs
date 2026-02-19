using System.Text.Json.Serialization;

namespace ClientLibrary.Models.Audit;

public class AuditLog
{
    public Guid Id { get; set; }

    [JsonPropertyName("userId")]
    public string? UserId { get; set; }

    [JsonPropertyName("userName")]
    public string? UserName { get; set; }

    [JsonPropertyName("userRole")]
    public string? UserRole { get; set; }

    [JsonPropertyName("action")]
    public string? Action { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("entityName")]
    public string? EntityName { get; set; }

    [JsonPropertyName("entityId")]
    public string? EntityId { get; set; }

    [JsonPropertyName("oldValues")]
    public string? OldValues { get; set; }

    [JsonPropertyName("newValues")]
    public string? NewValues { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("ipAddress")]
    public string? IpAddress { get; set; }

    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; }

    [JsonPropertyName("failureReason")]
    public string? FailureReason { get; set; }
}

public class AuditLogFilter
{
    public string? UserId { get; set; }
    public string? Action { get; set; }
    public string? EntityName { get; set; }
    public bool? IsSuccess { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

public class AuditLogResult
{
    [JsonPropertyName("data")]
    public List<AuditLog> Items { get; set; } = new();

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}
