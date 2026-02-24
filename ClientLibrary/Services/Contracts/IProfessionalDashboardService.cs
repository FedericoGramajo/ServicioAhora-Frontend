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
using ClientLibrary.Models.ProfessionalLicense;

namespace ClientLibrary.Services.Contracts;

public interface IProfessionalDashboardService
{
    Task<DashboardMetrics> GetDashboardMetricsAsync();
    Task<List<ServiceGroup>> GetServiceGroupsAsync();
    Task<List<ServiceTransaction>> GetTransactionsAsync(DateTime start, DateTime end, string? status, string? city);
    Task<ServiceResponse> AddServiceAsync(CreateServiceOffering service);
    Task<ServiceResponse> UpdateServiceAsync(UpdateServiceOffering service);
    Task<ServiceResponse> DeleteServiceAsync(string serviceSlug);
    Task<List<GetServiceOffering>> GetServiceOfferingsByProfessionalAsync(string professionalId);

    // Category Management
    Task<List<string>> GetAvailableCategoriesAsync();
    Task<List<GetProfessionalCategory>> GetMyCategoriesAsync(string professionalId);
    Task<ServiceResponse> AddMyCategoryAsync(Guid categoryId);
    Task<ServiceResponse> RemoveMyCategoryAsync(string professionalId, Guid categoryId);

    // Availability Management
    Task<List<ProfessionalAvailability>> GetAvailabilityAsync(string professionalId);
    Task<ServiceResponse> AddAvailabilityAsync(AddAvailabilityRequest request);
    Task<ServiceResponse> SetDailyAvailabilityAsync(BatchAvailabilityDto request);
    Task<ServiceResponse> RemoveAvailabilityAsync(Guid id);

    // Certification/License Management
    Task<List<GetProfessionalLicense>> GetLicensesAsync(string professionalId);
    Task<ServiceResponse> AddLicenseAsync(CreateProfessionalLicense license);
    Task<ServiceResponse> UpdateLicenseAsync(UpdateProfessionalLicense license);
    Task<ServiceResponse> RemoveLicenseAsync(Guid id);

    // Booking Management
    Task<List<GetBooking>> GetBookingsAsync(string professionalId);
    Task<ServiceResponse> AcceptBookingAsync(Guid bookingId);
    Task<ServiceResponse> CancelBookingAsync(Guid bookingId);
    Task<ServiceResponse> StartBookingAsync(Guid bookingId);
    Task<ServiceResponse> FinishBookingAsync(Guid bookingId);
}
