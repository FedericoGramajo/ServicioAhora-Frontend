namespace ClientLibrary.Models
{
    public record ServiceResponse(bool success = false, string Message = null!);
    public record ServiceResponse<T>(bool success, string Message = null!, T? Data = default);
}
