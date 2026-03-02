using System;
using System.ComponentModel.DataAnnotations;

namespace ClientLibrary.Models.Booking
{
    public class CreateRatingDto
    {
        [Required]
        public Guid ServiceId { get; set; }
        
        [Range(1, 5)]
        public int Score { get; set; }
        
        [MaxLength(500)]
        public string? Comment { get; set; }
    }

    public class GetRatingDto
    {
        public Guid Id { get; set; }
        public string? CustomerName { get; set; }
        public string? ProfessionalName { get; set; }
        public Guid ServiceId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
