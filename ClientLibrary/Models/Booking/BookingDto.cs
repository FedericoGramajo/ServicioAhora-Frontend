using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ClientLibrary.Models.Booking
{
    public class CreateBooking
    {
        [Required]
        public string ProfessionalId { get; set; } = default!;
        [Required]
        public string CustomerId { get; set; } = default!;
        [Required]
        public DateTime Date { get; set; }
        public string? ClientAddress { get; set; }
        public List<CreateBookingDetailDto> Details { get; set; } = new();
    }

    public class CreateBookingDetailDto
    {
        [Required]
        public Guid ServiceOfferingId { get; set; }
        public int Quantity { get; set; } = 1;
        public string? Description { get; set; }
        public string? ServiceCategory { get; set; }
        public decimal? UnitPrice { get; set; }
    }

    public class GetBooking
    {
        public Guid Id { get; set; }
        public string ProfessionalId { get; set; } = string.Empty;
        public string ProfessionalName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public string ClientAddress { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<GetBookingDetail> Details { get; set; } = new();
        public bool IsRated { get; set; }

        // Mapeos de conveniencia para la UI actual e integración con Patrón State
        public Guid ServiceId => Details.FirstOrDefault()?.ServiceId ?? Guid.Empty;
        public string ServiceName => Details.FirstOrDefault()?.ServiceName ?? "Sin servicio";
        public string CategoryName => Details.FirstOrDefault()?.ServiceCategory ?? "General";
        public decimal TotalAmount => Details.Sum(d => d.UnitPrice * d.Quantity);
        public string Status => StatusName ?? "Unknown";
        public DateTime ScheduledDate => Date;
        public States.IBookingState State => States.BookingStateFactory.Create(Status);
    }

    public class GetBookingDetail
    {
        public Guid ServiceId { get; set; }
        public Guid ServiceOfferingId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceCategory { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
