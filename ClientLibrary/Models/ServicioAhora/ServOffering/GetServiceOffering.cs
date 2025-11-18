namespace ClientLibrary.Models.ServicioAhora.ServOffering
{
    public class GetServiceOffering : BaseServiceOffering
    {
            public Guid Id { get; set; }
            public string ProfessionalId { get; set; } = default!;

            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }

            public string ProfessionalFullName { get; set; } = string.Empty;
            public string CategoryName { get; set; } = string.Empty;
    }
    
}
