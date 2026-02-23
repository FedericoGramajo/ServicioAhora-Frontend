using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Booking;
using ClientLibrary.Models.Category;
using ClientLibrary.Models.Landing;
using ClientLibrary.Models.ProfessionalCat;
using ClientLibrary.Models.ServicioAhora.ServOffering;
using ClientLibrary.Models.ProfessionalLicense;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations;

public class ProfessionalDashboardService(IHttpClientHelper httpClient, IApiCallHelper apiHelper) : IProfessionalDashboardService
{
    public async Task<DashboardMetrics> GetDashboardMetricsAsync()
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.GetMetrics,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<DashboardMetrics>(result) ?? new DashboardMetrics(0, 0, 0, 0.0);
    }

    public async Task<List<ServiceGroup>> GetServiceGroupsAsync()
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.GetServiceGroups,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<ServiceGroup>>(result) ?? new List<ServiceGroup>();
    }

    public async Task<List<ServiceTransaction>> GetTransactionsAsync(DateTime start, DateTime end, string? status, string? city)
    {
        var client = await httpClient.GetPrivateClientAsync();
        // El backend espera startDate y endDate (nombres del [FromQuery] del DashboardController)
        string query = $"?startDate={start:yyyy-MM-dd}&endDate={end:yyyy-MM-dd}";
        if (!string.IsNullOrEmpty(status)) query += $"&status={Uri.EscapeDataString(status)}";
        if (!string.IsNullOrEmpty(city)) query += $"&city={Uri.EscapeDataString(city)}";

        var apiCall = new ApiCall
        {
            Route = Constant.Professional.GetTransactions + query,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<ServiceTransaction>>(result) ?? new List<ServiceTransaction>();
    }

    public async Task AddServiceAsync(CreateServiceOffering service)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.AddService,
            Type = Constant.ApiCallType.Post,
            Client = client,
            Model = service
        };
        await apiHelper.ApiCallTypeCall<CreateServiceOffering>(apiCall);
    }

    public async Task<ServiceResponse> UpdateServiceAsync(UpdateServiceOffering service)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.UpdateService,
            Type = Constant.ApiCallType.Update,
            Client = client,
            Model = service
        };
        var result = await apiHelper.ApiCallTypeCall<UpdateServiceOffering>(apiCall);
        return result == null ? apiHelper.ConnectionError() : await apiHelper.GetServiceResponse<ServiceResponse>(result);
    }

    public async Task DeleteServiceAsync(string serviceSlug)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.DeleteService,
            Type = Constant.ApiCallType.Delete,
            Client = client
        };
        apiCall.ToString(serviceSlug);
        await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
    }

    public async Task<List<GetServiceOffering>> GetServiceOfferingsByProfessionalAsync(string professionalId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.ServiceOffering.GetByProfessional,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        apiCall.ToString(professionalId);
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<GetServiceOffering>>(result) ?? new List<GetServiceOffering>();
    }

    // Category Management
    public async Task<List<string>> GetAvailableCategoriesAsync()
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.GetAvailableCategories,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<string>>(result) ?? new List<string>();
    }

    public async Task<List<GetProfessionalCategory>> GetMyCategoriesAsync(string professionalId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.GetMyCategories,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        apiCall.ToString(professionalId);
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<GetProfessionalCategory>>(result) ?? new List<GetProfessionalCategory>();
    }

    public async Task<ServiceResponse> AddMyCategoryAsync(Guid categoryId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var professionalId = await httpClient.GetUserIdAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.AddMyCategory,
            Type = Constant.ApiCallType.Post,
            Client = client,
            Model = new CreateProfessionalCategory
            {
                CategoryId = categoryId,
                ProfessionalId = professionalId ?? string.Empty
            }
        };
        var result = await apiHelper.ApiCallTypeCall<object>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? new ServiceResponse(false, "Error al agregar el rubro");
    }

    public async Task<ServiceResponse> RemoveMyCategoryAsync(string professionalId, Guid categoryId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = $"{Constant.Professional.RemoveMyCategory}/{professionalId}/{categoryId}",
            Type = Constant.ApiCallType.Delete,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);

        if (result != null && result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return new ServiceResponse(false, "No se puede eliminar la categoría porque tiene servicios asignados. Elimine los servicios primero.");
        }

        return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? new ServiceResponse(false, "Error al eliminar el rubro");
    }

    // Availability Management
    public async Task<List<ProfessionalAvailability>> GetAvailabilityAsync(string professionalId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route =Constant.Professional.GetAvailability,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        apiCall.ToString(professionalId);
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<ProfessionalAvailability>>(result) ?? new List<ProfessionalAvailability>();
    }

    public async Task<ServiceResponse> AddAvailabilityAsync(AddAvailabilityRequest request)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.AddAvailability,
            Type = Constant.ApiCallType.Post,
            Client = client,
            Model = request
        };
        var result = await apiHelper.ApiCallTypeCall<AddAvailabilityRequest>(apiCall);
        
        if (result == null)
            return new ServiceResponse(false, "Error de conexión con el servidor");

        if (result.IsSuccessStatusCode)
        {
            var response = await apiHelper.GetServiceResponse<ServiceResponse>(result);
            return response ?? new ServiceResponse(true, "Horario agregado correctamente");
        }
        else
        {
            var response = await apiHelper.GetServiceResponse<ServiceResponse>(result);
            return response ?? new ServiceResponse(false, $"Error del servidor: {result.StatusCode}");
        }
    }



    public async Task<ServiceResponse> SetDailyAvailabilityAsync(BatchAvailabilityDto request)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.BatchAVailability,
            Type = Constant.ApiCallType.Post,
            Client = client,
            Model = request
        };
        var result = await apiHelper.ApiCallTypeCall<BatchAvailabilityDto>(apiCall);

        if (result == null)
            return new ServiceResponse(false, "Error de conexión con el servidor");

        if (result.IsSuccessStatusCode)
        {
             // Try to deserialize ServiceResponse, or create one if body is empty but status is OK
             var response = await apiHelper.GetServiceResponse<ServiceResponse>(result);
             return response ?? new ServiceResponse(true, "Disponibilidad actualizada correctamente");
        }
        else
        {
            var response = await apiHelper.GetServiceResponse<ServiceResponse>(result);
            return response ?? new ServiceResponse(false, $"Error del servidor: {result.StatusCode}");
        }
    }

    public async Task<ServiceResponse> RemoveAvailabilityAsync(Guid id)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.RemoveAvailability,
            Type = Constant.ApiCallType.Delete,
            Client = client
        };
        apiCall.ToString(id);
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        
        if (result == null)
            return new ServiceResponse(false, "Error de conexión con el servidor");

        if (result.IsSuccessStatusCode)
        {
            var response = await apiHelper.GetServiceResponse<ServiceResponse>(result);
            return response ?? new ServiceResponse(true, "Horario eliminado correctamente");
        }
        else
        {
            var response = await apiHelper.GetServiceResponse<ServiceResponse>(result);
            return response ?? new ServiceResponse(false, $"Error del servidor: {result.StatusCode}");
        }
    }
    
    // Certification/License Management
    public async Task<List<GetProfessionalLicense>> GetLicensesAsync(string professionalId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.ProfessionalLicense.GetAll,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        apiCall.ToString(professionalId);
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<GetProfessionalLicense>>(result) ?? new List<GetProfessionalLicense>();
    }

    public async Task<ServiceResponse> AddLicenseAsync(CreateProfessionalLicense license)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.ProfessionalLicense.Add,
            Type = Constant.ApiCallType.Post,
            Client = client,
            Model = license
        };
        var result = await apiHelper.ApiCallTypeCall<CreateProfessionalLicense>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? new ServiceResponse(false, "Error al agregar la licencia");
    }

    public async Task<ServiceResponse> UpdateLicenseAsync(UpdateProfessionalLicense license)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.ProfessionalLicense.Update,
            Type = Constant.ApiCallType.Update,
            Client = client,
            Model = license
        };
        var result = await apiHelper.ApiCallTypeCall<UpdateProfessionalLicense>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? new ServiceResponse(false, "Error al actualizar la licencia");
    }

    public async Task<ServiceResponse> RemoveLicenseAsync(Guid id)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.ProfessionalLicense.Delete,
            Type = Constant.ApiCallType.Delete,
            Client = client
        };
        apiCall.ToString(id);
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? new ServiceResponse(false, "Error al eliminar la licencia");
    }

    // Booking Management
    public async Task<List<GetBooking>> GetBookingsAsync(string professionalId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Booking.GetByProfessional,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        apiCall.ToString(professionalId);
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<GetBooking>>(result) ?? new List<GetBooking>();
    }

    public async Task<ServiceResponse> AcceptBookingAsync(Guid bookingId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = $"{Constant.Booking.UpdateStatus}/{bookingId}?status={Constant.BookingStatus.Confirmed}",
            Type = Constant.ApiCallType.Update,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? apiHelper.ConnectionError();
    }

    public async Task<ServiceResponse> CancelBookingAsync(Guid bookingId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = $"{Constant.Booking.UpdateStatus}/{bookingId}?status={Constant.BookingStatus.Canceled}",
            Type = Constant.ApiCallType.Update,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? apiHelper.ConnectionError();
    }

    public async Task<ServiceResponse> StartBookingAsync(Guid bookingId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = $"{Constant.Booking.UpdateStatus}/{bookingId}?status={Constant.BookingStatus.InProgress}",
            Type = Constant.ApiCallType.Update,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? apiHelper.ConnectionError();
    }

    public async Task<ServiceResponse> FinishBookingAsync(Guid bookingId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = $"{Constant.Booking.UpdateStatus}/{bookingId}?status={Constant.BookingStatus.Completed}",
            Type = Constant.ApiCallType.Update,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? apiHelper.ConnectionError();
    }
}
