using ClientLibrary.Models.Audit;

namespace ClientLibrary.Services.Contracts;

public interface IAuditLogService
{
    Task<AuditLogResult> GetAllAsync(AuditLogFilter filter);
}
