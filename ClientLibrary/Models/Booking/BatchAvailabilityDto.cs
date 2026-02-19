using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientLibrary.Models.Booking
{
    public class BatchAvailabilityDto
    {
        [Required]
        public string ProfessionalId { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        public List<string> Hours { get; set; } = new();

        public int SlotDurationMinutes { get; set; } = 60;
    }
}
