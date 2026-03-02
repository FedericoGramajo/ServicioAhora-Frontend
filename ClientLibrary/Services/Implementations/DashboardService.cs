using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Dashboard;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations;

public class DashboardService(IHttpClientHelper httpClientHelper, IApiCallHelper apiCallHelper) : IDashboardService
{
    public async Task<ServiceResponse<AdminReportsResponse>> GetAdminReportsAsync()
    {
        try
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Client = client,
                Type = Constant.ApiCallType.Get,
                Route = Constant.Admin.GetAdminReports
            };
            var response = await apiCallHelper.ApiCallTypeCall<object>(apiCall);
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await apiCallHelper.GetServiceResponse<AdminReportsResponse>(response);
                    return new ServiceResponse<AdminReportsResponse>(true, "Operación exitosa", data);
                }
                var errorMsg = await response.Content.ReadAsStringAsync();
                return new ServiceResponse<AdminReportsResponse>(false, $"Error API ({response.StatusCode}): {errorMsg}");
            }
            return new ServiceResponse<AdminReportsResponse>(false, "No se recibió respuesta del servidor");
        }
        catch (Exception ex)
        {
            return new ServiceResponse<AdminReportsResponse>(false, $"Excepción: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<ProfessionalReportsResponse>> GetProfessionalReportsAsync()
    {
        try
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Client = client,
                Type = Constant.ApiCallType.Get,
                Route = Constant.Admin.GetProfessionalReports
            };
            var response = await apiCallHelper.ApiCallTypeCall<object>(apiCall);
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await apiCallHelper.GetServiceResponse<ProfessionalReportsResponse>(response);
                    return new ServiceResponse<ProfessionalReportsResponse>(true, "Operación exitosa", data);
                }
                var errorMsg = await response.Content.ReadAsStringAsync();
                return new ServiceResponse<ProfessionalReportsResponse>(false, $"Error API ({response.StatusCode}): {errorMsg}");
            }
            return new ServiceResponse<ProfessionalReportsResponse>(false, "No se recibió respuesta del servidor");
        }
        catch (Exception ex)
        {
            return new ServiceResponse<ProfessionalReportsResponse>(false, $"Excepción: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<CustomerReportsResponse>> GetCustomerReportsAsync()
    {
        try
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Client = client,
                Type = Constant.ApiCallType.Get,
                Route = Constant.Admin.GetCustomerReports
            };
            var response = await apiCallHelper.ApiCallTypeCall<object>(apiCall);
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await apiCallHelper.GetServiceResponse<CustomerReportsResponse>(response);
                    return new ServiceResponse<CustomerReportsResponse>(true, "Operación exitosa", data);
                }
                var errorMsg = await response.Content.ReadAsStringAsync();
                return new ServiceResponse<CustomerReportsResponse>(false, $"Error API ({response.StatusCode}): {errorMsg}");
            }
            return new ServiceResponse<CustomerReportsResponse>(false, "No se recibió respuesta del servidor");
        }
        catch (Exception ex)
        {
            return new ServiceResponse<CustomerReportsResponse>(false, $"Excepción: {ex.Message}");
        }
    }
}
