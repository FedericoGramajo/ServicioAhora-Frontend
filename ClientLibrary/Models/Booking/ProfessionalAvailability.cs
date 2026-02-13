using System;

namespace ClientLibrary.Models.Booking;

public class ProfessionalAvailability
{
    public Guid Id { get; set; }
    public string ProfessionalId { get; set; } = string.Empty;
    
    public DayOfWeek DayOfWeek { get; set; } 
    public TimeSpan StartTime { get; set; } 
    public TimeSpan EndTime { get; set; } 
    
    public bool IsHoliday { get; set; } 
    public DateTime? SpecificDate { get; set; } 
}
