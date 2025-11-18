namespace ClientLibrary.Models
{
    public record ServiceResponse(bool success = false, string Message = null!)
    {
    }
}
