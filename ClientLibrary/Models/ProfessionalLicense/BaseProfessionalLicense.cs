using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Models.ProfessionalLicense
{
    public class BaseProfessionalLicense
    {
        [Required(ErrorMessage = "El número de Certificacion es obligatoria.")]
        public string Number { get; set; } = string.Empty;
        [Required(ErrorMessage = "El emisor de la Certificacion es obligatorio.")]
        public string? Issuer { get; set; }
        [Required(ErrorMessage = "La fecha de emisión de la Certificacion es obligatoria.")]
        public DateTime IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? Image { get; set; }
    }
}
