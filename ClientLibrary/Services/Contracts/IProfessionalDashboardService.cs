using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientLibrary.Models.Landing;
using ClientLibrary.Models;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Models.Booking;
using ClientLibrary.Models.Category;
using ClientLibrary.Models.ProfessionalCat;
using ClientLibrary.Models.ServicioAhora.ServOffering;

namespace ClientLibrary.Services.Contracts;

public interface IProfessionalDashboardService
{
    Task<DashboardMetrics> GetDashboardMetricsAsync();
    Task<List<ServiceGroup>> GetServiceGroupsAsync();
    Task<List<ServiceTransaction>> GetTransactionsAsync(DateTime start, DateTime end, string? status, string? city);
    Task AddServiceAsync(CreateServiceOffering service);
    Task UpdateServiceAsync(ServiceFormModel service);
    Task DeleteServiceAsync(string serviceSlug);
    Task<List<GetServiceOffering>> GetServiceOfferingsByProfessionalAsync(string professionalId);

    // Category Management
    Task<List<string>> GetAvailableCategoriesAsync();
    Task<List<GetProfessionalCategory>> GetMyCategoriesAsync(string professionalId);
    Task<ServiceResponse> AddMyCategoryAsync(Guid categoryId);
    Task<ServiceResponse> RemoveMyCategoryAsync(string professionalId, Guid categoryId);

    // Availability Management
    Task<List<ProfessionalAvailability>> GetAvailabilityAsync(string professionalId);
    Task<ServiceResponse> AddAvailabilityAsync(AddAvailabilityRequest request);
    Task<ServiceResponse> RemoveAvailabilityAsync(Guid id);

    // Certification Management
    Task<List<CertificationModel>> GetCertificationsAsync();
    Task<ServiceResponse> AddCertificationAsync(CertificationModel certification);
    Task<ServiceResponse> RemoveCertificationAsync(Guid id);
}
