using System;

namespace ClientLibrary.Models.Booking;

public class Appointment
{
    public Guid Id { get; set; }
    
    public string CustomerId { get; set; } = string.Empty;
    public string ProfessionalId { get; set; } = string.Empty;
    public string ServiceOfferingId { get; set; } = string.Empty; // Changed to string to match Slug/String ID usage in frontend
    
    public DateTime ScheduledDate { get; set; } 
    public int DurationMinutes { get; set; } 
    
    public AppointmentStatus Status { get; set; } = AppointmentStatus.PendingPayment;
    public string? PaymentReference { get; set; } 
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum AppointmentStatus 
{ 
    PendingPayment, 
    Confirmed, 
    Cancelled, 
    Completed 
}
