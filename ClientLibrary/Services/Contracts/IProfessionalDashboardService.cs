using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientLibrary.Models.Landing;
using ClientLibrary.Models;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Models.Booking;

namespace ClientLibrary.Services.Contracts;

public interface IProfessionalDashboardService
{
    Task<DashboardMetrics> GetDashboardMetricsAsync();
    Task<List<ServiceGroup>> GetServiceGroupsAsync();
    Task<List<ServiceTransaction>> GetTransactionsAsync(DateTime start, DateTime end, string? status, string? city);
    Task AddServiceAsync(ServiceFormModel service);
    Task UpdateServiceAsync(ServiceFormModel service);
    Task DeleteServiceAsync(string serviceSlug);

    // Category Management
    Task<List<string>> GetAvailableCategoriesAsync();
    Task<List<string>> GetMyCategoriesAsync();
    Task<ServiceResponse> AddMyCategoryAsync(string category);
    Task<ServiceResponse> RemoveMyCategoryAsync(string category);

    // Availability Management
    Task<List<ProfessionalAvailability>> GetAvailabilityAsync();
    Task<ServiceResponse> AddAvailabilityAsync(ProfessionalAvailability availability);
    Task<ServiceResponse> RemoveAvailabilityAsync(int dayOfWeek);
    Task<ServiceResponse> RemoveAvailabilityAsync(Guid id);
}

