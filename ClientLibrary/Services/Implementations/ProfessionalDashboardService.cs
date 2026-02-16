using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientLibrary.Models.Landing;
using ClientLibrary.Models;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Models.Booking;

namespace ClientLibrary.Services.Implementations;

public class ProfessionalDashboardService : IProfessionalDashboardService
{
    // In-Memory Storage (Mock DB)
    private readonly List<ServiceTransaction> _transactions = new();
    private readonly List<ServiceGroup> _serviceGroups = new();
    
    // Category Management
    private readonly List<string> _myCategories = new();
    private readonly List<string> _allAvailableCategories = new()
    {
        "Limpieza y orden", "Mantenimiento y reparaciones", "Jardinería", "Decoración", 
        "Plomería", "Electricidad", "Gasista", "Albañilería", "Fletes y Mudanzas", 
        "Belleza y Estética", "Cuidado de Personas", "Mecánica Automotriz", "Informática"
    };

    public ProfessionalDashboardService()
    {
        // NO MOCK DATA GENERATION - Start Empty as requested
        // Initialize with 0 transactions and empty service groups
    }

    public Task<DashboardMetrics> GetDashboardMetricsAsync()
    {
        var metrics = new DashboardMetrics(
            ActiveServices: _serviceGroups.Sum(g => g.Services.Count),
            NewRequests: 0, 
            InProgress: 0,  
            Rating: 0.0
        );
        return Task.FromResult(metrics);
    }

    public Task<List<ServiceGroup>> GetServiceGroupsAsync()
    {
        // Return only service groups for categories I have selected
        return Task.FromResult(_serviceGroups);
    }

    public Task<List<ServiceTransaction>> GetTransactionsAsync(DateTime start, DateTime end, string? status, string? city)
    {
        var query = _transactions
            .Where(t => t.Date >= start && t.Date <= end);

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(t => t.Status == status);
        }

        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(t => t.City == city);
        }

        return Task.FromResult(query.ToList());
    }

    public Task AddServiceAsync(ServiceFormModel service)
    {
        // Add to the correct group
        var group = _serviceGroups.FirstOrDefault(g => g.Category == service.Category);
        if (group == null)
        {
            // If group doesn't exist (maybe category was just added), create it
            // Only if it is in my categories
            if (_myCategories.Contains(service.Category))
            {
                group = new ServiceGroup(service.Category, new List<ServiceSummary>());
                _serviceGroups.Add(group);
            }
            else
            {
                // Can't add service to a category I don't have
                return Task.CompletedTask; 
            }
        }
        
        group.Services.Add(new ServiceSummary(
            Guid.NewGuid().ToString(), 
            service.Name, 
            service.Price, 
            service.Description
        ));

        return Task.CompletedTask;
    }

    public Task UpdateServiceAsync(ServiceFormModel service)
    {
        if (string.IsNullOrEmpty(service.Slug)) return Task.CompletedTask;

        var group = _serviceGroups.FirstOrDefault(g => g.Category == service.Category);
        if (group != null)
        {
            var existingService = group.Services.FirstOrDefault(s => s.Slug == service.Slug);
            if (existingService != null)
            {
                // Records are immutable, so we must replace it
                var newService = new ServiceSummary(
                    existingService.Slug,
                    service.Name,
                    service.Price,
                    service.Description
                );

                var index = group.Services.IndexOf(existingService);
                group.Services[index] = newService;
            }
        }
        return Task.CompletedTask;
    }

    public Task DeleteServiceAsync(string serviceSlug)
    {
        return Task.CompletedTask;
    }

    // Category Management
    public Task<List<string>> GetAvailableCategoriesAsync()
    {
        return Task.FromResult(_allAvailableCategories);
    }

    public Task<List<string>> GetMyCategoriesAsync()
    {
        return Task.FromResult(_myCategories);
    }

    public Task<ServiceResponse> AddMyCategoryAsync(string category)
    {
        if (_myCategories.Count >= 2)
        {
            return Task.FromResult(new ServiceResponse(false, "Solo puedes seleccionar un máximo de 2 rubros."));
        }

        if (_myCategories.Contains(category))
        {
             return Task.FromResult(new ServiceResponse(false, "Ya tienes este rubro seleccionado."));
        }

        _myCategories.Add(category);
        
        // Create an empty service group for this category so it shows up
        if (!_serviceGroups.Any(g => g.Category == category))
        {
            _serviceGroups.Add(new ServiceGroup(category, new List<ServiceSummary>()));
        }

        return Task.FromResult(new ServiceResponse(true, "Rubro agregado correctamente."));
    }

    public Task<ServiceResponse> RemoveMyCategoryAsync(string category)
    {
        if (_myCategories.Remove(category))
        {
            // Also remove the service group and its services? 
            // For now, let's keep it simple and remove the group.
            var group = _serviceGroups.FirstOrDefault(g => g.Category == category);
            if (group != null)
            {
                _serviceGroups.Remove(group);
            }
            return Task.FromResult(new ServiceResponse(true, "Rubro eliminado correctamente."));
        }
        return Task.FromResult(new ServiceResponse(false, "No se encontró el rubro."));
    }

    // Availability Management
    private readonly List<ProfessionalAvailability> _availabilities = new();

    public Task<List<ProfessionalAvailability>> GetAvailabilityAsync()
    {
        return Task.FromResult(_availabilities.OrderBy(a => a.DayOfWeek).ToList());
    }

    public Task<ServiceResponse> AddAvailabilityAsync(ProfessionalAvailability availability)
    {
        bool isDuplicate = false;
        if (availability.SpecificDate.HasValue)
        {
            isDuplicate = _availabilities.Any(a => 
                a.SpecificDate.HasValue && 
                a.SpecificDate.Value.Date == availability.SpecificDate.Value.Date && 
                a.StartTime == availability.StartTime && 
                a.EndTime == availability.EndTime);
        }
        else
        {
             isDuplicate = _availabilities.Any(a => 
                !a.SpecificDate.HasValue &&
                a.DayOfWeek == availability.DayOfWeek && 
                a.StartTime == availability.StartTime && 
                a.EndTime == availability.EndTime);
        }

        if (isDuplicate)
        {
            return Task.FromResult(new ServiceResponse(false, "Este horario ya está agregado."));
        }

        availability.Id = Guid.NewGuid(); // Assign ID
        _availabilities.Add(availability);
        return Task.FromResult(new ServiceResponse(true, "Horario agregado correctamente."));
    }

    public Task<ServiceResponse> RemoveAvailabilityAsync(int dayOfWeek)
    {
        // Interface uses int dayOfWeek, model uses DayOfWeek enum. Casting needed.
        var targetDay = (DayOfWeek)dayOfWeek;
        
        var slots = _availabilities.Where(a => a.DayOfWeek == targetDay).ToList();
        if (slots.Any())
        {
            foreach(var slot in slots)
            {
                _availabilities.Remove(slot);
            }
            return Task.FromResult(new ServiceResponse(true, "Horarios eliminados para el día seleccionado."));
        }
        return Task.FromResult(new ServiceResponse(false, "No se encontraron horarios para ese día."));
    }

    public Task<ServiceResponse> RemoveAvailabilityAsync(Guid id)
    {
        var slot = _availabilities.FirstOrDefault(a => a.Id == id);
        if (slot != null)
        {
            _availabilities.Remove(slot);
            return Task.FromResult(new ServiceResponse(true, "Horario eliminado."));
        }
        return Task.FromResult(new ServiceResponse(false, "No se encontró el horario."));
    }
}
