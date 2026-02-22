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
        // Construct query string manually or via helper if available
        // Simple manual construction for now
        string query = $"?start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}";
        if (!string.IsNullOrEmpty(status)) query += $"&status={status}";
        if (!string.IsNullOrEmpty(city)) query += $"&city={city}";

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
            Route = $"{Constant.Professional.DeleteService}/{serviceSlug}",
            Type = Constant.ApiCallType.Delete,
            Client = client
        };
        await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
    }

    public async Task<List<GetServiceOffering>> GetServiceOfferingsByProfessionalAsync(string professionalId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = $"{Constant.ServiceOffering.GetByProfessional}/{professionalId}",
            Type = Constant.ApiCallType.Get,
            Client = client
        };
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
            Route = $"{Constant.Professional.GetMyCategories}/{professionalId}",
            Type = Constant.ApiCallType.Get,
            Client = client
        };
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
            Route = $"{Constant.Professional.GetAvailability}/{professionalId}",
            Type = Constant.ApiCallType.Get,
            Client = client
        };
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
            Route = $"{Constant.Professional.RemoveAvailability}/{id}",
            Type = Constant.ApiCallType.Delete,
            Client = client
        };
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
    
    // Certification Management
    public async Task<List<CertificationModel>> GetCertificationsAsync()
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.GetCertifications,
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<List<CertificationModel>>(result) ?? new List<CertificationModel>();
    }

    public async Task<ServiceResponse> AddCertificationAsync(CertificationModel certification)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = Constant.Professional.AddCertification,
            Type = Constant.ApiCallType.Post,
            Client = client,
            Model = certification
        };
        var result = await apiHelper.ApiCallTypeCall<CertificationModel>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result);
    }

    public async Task<ServiceResponse> RemoveCertificationAsync(Guid id)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = $"{Constant.Professional.RemoveCertification}/{id}",
            Type = Constant.ApiCallType.Delete,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse>(result);
    }

    // Booking Management
    public async Task<ServiceResponse<IEnumerable<BookingDto>>> GetBookingsAsync(string professionalId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = $"{Constant.Booking.GetByProfessional}/{professionalId}",
            Type = Constant.ApiCallType.Get,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        return await apiHelper.GetServiceResponse<ServiceResponse<IEnumerable<BookingDto>>>(result);
    }

    public async Task<ServiceResponse> AcceptBookingAsync(Guid bookingId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = $"{Constant.Booking.Accept}/{bookingId}",
            Type = Constant.ApiCallType.Post,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        var response = result == null ? apiHelper.ConnectionError() : await apiHelper.GetServiceResponse<ServiceResponse>(result);
        return response ?? new ServiceResponse(false, "Error inesperado al aceptar la reserva");
    }

    public async Task<ServiceResponse> CancelBookingAsync(Guid bookingId)
    {
        var client = await httpClient.GetPrivateClientAsync();
        var apiCall = new ApiCall
        {
            Route = $"{Constant.Booking.Cancel}/{bookingId}",
            Type = Constant.ApiCallType.Post,
            Client = client
        };
        var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
        var response = result == null ? apiHelper.ConnectionError() : await apiHelper.GetServiceResponse<ServiceResponse>(result);
        return response ?? new ServiceResponse(false, "Error inesperado al cancelar la reserva");
    }
}
