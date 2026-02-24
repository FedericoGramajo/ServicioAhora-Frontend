using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Models.ServicioAhora.ServOffering
{
        public class BaseServiceOffering
        {
            [Required(ErrorMessage = "El nombre del Servicio es obligatorio."), MaxLength(150)]
            public string Name { get; set; } = string.Empty;

            [MaxLength(500)]
            public string? Description { get; set; }

            [Required(ErrorMessage = "El precio del Servicio es obligatoria."), Range(0, double.MaxValue)]
            public decimal BasePrice { get; set; }

            [Required(ErrorMessage = "La duración estimada es obligatoria.")]
            [Range(1, int.MaxValue, ErrorMessage = "La duración debe ser al menos de una hora.")]
            public int? EstimatedDuration { get; set; }

            public bool Status { get; set; } = true;
            [Required]
            public Guid CategoryId { get; set; }

            public string? Image { get; set; }
    }
    
}
