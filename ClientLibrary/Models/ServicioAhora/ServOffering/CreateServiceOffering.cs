using System.ComponentModel.DataAnnotations;

namespace ClientLibrary.Models.ServicioAhora.ServOffering
{
    public class CreateServiceOffering : BaseServiceOffering
        {
            [Required]
            public string ProfessionalId { get; set; } = default!;
        }
    
}
