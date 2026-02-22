using ClientLibrary.Models.Booking;

namespace ClientLibrary.Services.Implementations;

public class AvailabilityMockService
{
    // Simulating availability: Mon-Fri, 9:00 - 17:00
    public Task<List<ProfessionalAvailability>> GetAvailabilityAsync(string professionalId)
    {
        var availability = new List<ProfessionalAvailability>();
        var today = DateTime.Today;
        
        // Generate availability for next 14 days
        for (int i = 0; i < 14; i++)
        {
            var date = today.AddDays(i);
            if (date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday)
            {
                var endTime = date.DayOfWeek == DayOfWeek.Friday ? new TimeSpan(16, 0, 0) : new TimeSpan(17, 0, 0);
                availability.Add(new ProfessionalAvailability 
                { 
                    ProfessionalId = professionalId, 
                    Date = date,
                    DayOfWeek = date.DayOfWeek, 
                    StartTime = new TimeSpan(9, 0, 0), 
                    EndTime = endTime 
                });
            }
        }

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
