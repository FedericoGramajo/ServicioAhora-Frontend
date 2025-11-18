using System.ComponentModel.DataAnnotations;

namespace ClientLibrary.Models.ServicioAhora.ServOffering
{
    public sealed class UpdateServiceOffering : BaseServiceOffering
        {
            [Required]
            public Guid Id { get; set; }
        }
    
}
