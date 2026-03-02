using ClientLibrary.Models;
using ClientLibrary.Models.Dashboard;

namespace ClientLibrary.Services.Contracts;

public interface IDashboardService
{
    Task<ServiceResponse<AdminReportsResponse>> GetAdminReportsAsync();
    Task<ServiceResponse<ProfessionalReportsResponse>> GetProfessionalReportsAsync();
    Task<ServiceResponse<CustomerReportsResponse>> GetCustomerReportsAsync();
}
