using System.Collections.Generic;
using System.Threading.Tasks;
using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Authentication.Rol.Professional;
using ClientLibrary.Models.Landing;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations;

public class AdminDashboardService(IHttpClientHelper httpClient, IApiCallHelper apiHelper) : IAdminDashboardService
{
    public async Task<DashboardMetrics> GetMetricsAsync()
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Admin.GetMetrics,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<DashboardMetrics>(result) ?? new DashboardMetrics(0, 0, 0, 0, 0);
    }

    public async Task<List<ServiceTransaction>> GetTransactionsAsync(DateTime? start = null, DateTime? end = null, string? status = null, string? city = null)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var query = "";
        if (start.HasValue) query += $"{(string.IsNullOrEmpty(query) ? "?" : "&")}startDate={start.Value:yyyy-MM-dd}";
        if (end.HasValue) query += $"{(string.IsNullOrEmpty(query) ? "?" : "&")}endDate={end.Value:yyyy-MM-dd}";
        if (!string.IsNullOrEmpty(status)) query += $"{(string.IsNullOrEmpty(query) ? "?" : "&")}status={Uri.EscapeDataString(status)}";
        if (!string.IsNullOrEmpty(city)) query += $"{(string.IsNullOrEmpty(query) ? "?" : "&")}city={Uri.EscapeDataString(city)}";

        var apiCall = new ApiCall
        {
            Route = Constant.Admin.GetTransactions + query,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<ServiceTransaction>>(result) ?? new List<ServiceTransaction>();
    }

    public async Task<List<GetProfessional>> GetProfessionalsAsync()
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Admin.GetProfessionals,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<GetProfessional>>(result) ?? new List<GetProfessional>();
    }

    public async Task<ServiceResponse> UpdateProfessionalAsync(GetProfessional professional)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Admin.UpdateProfessional,
            Type = Constant.ApiCallType.Update,
            Client = client,
            Model = professional
        };
        var result = await apiHelper.ApiCallTypeCall<GetProfessional>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result);
    }

    public async Task<AdminReportsResponse> GetAdminReportsAsync()
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Admin.GetAdminReports,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<AdminReportsResponse>(result) ?? new AdminReportsResponse();
    }
}
