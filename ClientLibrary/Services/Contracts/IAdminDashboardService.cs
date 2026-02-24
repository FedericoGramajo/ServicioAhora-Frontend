using System.Collections.Generic;
using System.Threading.Tasks;
using ClientLibrary.Models;
using ClientLibrary.Models.Landing;
using ClientLibrary.Models.Authentication.Rol.Professional;

namespace ClientLibrary.Services.Contracts;

public interface IAdminDashboardService
{
    Task<DashboardMetrics> GetMetricsAsync();
    Task<List<ServiceTransaction>> GetTransactionsAsync(DateTime? start = null, DateTime? end = null, string? status = null, string? city = null);
    Task<List<GetProfessional>> GetProfessionalsAsync();
    Task<ServiceResponse> UpdateProfessionalAsync(GetProfessional professional);
}
