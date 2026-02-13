using ClientLibrary.Models.Booking;

namespace ClientLibrary.Services;

public class AvailabilityMockService
{
    // Simulating availability: Mon-Fri, 9:00 - 17:00
    public Task<List<ProfessionalAvailability>> GetAvailabilityAsync(string professionalId)
    {
        var availability = new List<ProfessionalAvailability>
        {
            new() { ProfessionalId = professionalId, DayOfWeek = DayOfWeek.Monday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
            new() { ProfessionalId = professionalId, DayOfWeek = DayOfWeek.Tuesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
            new() { ProfessionalId = professionalId, DayOfWeek = DayOfWeek.Wednesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
            new() { ProfessionalId = professionalId, DayOfWeek = DayOfWeek.Thursday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
            new() { ProfessionalId = professionalId, DayOfWeek = DayOfWeek.Friday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(16, 0, 0) } // Friday ends earlier
        };

        return Task.FromResult(availability);
    }

    public List<DateTime> GenerateTimeSlots(DateTime date, ProfessionalAvailability availability, int durationMinutes)
    {
        var slots = new List<DateTime>();
        
        if (availability == null) return slots;

        var start = date.Date.Add(availability.StartTime);
        var end = date.Date.Add(availability.EndTime);

        while (start.AddMinutes(durationMinutes) <= end)
        {
            // Simulate some taken slots (e.g. 12:00 is taken)
            if (start.Hour != 12) 
            {
                slots.Add(start);
            }
            start = start.AddMinutes(durationMinutes); 
        }

        return slots;
    }
}
