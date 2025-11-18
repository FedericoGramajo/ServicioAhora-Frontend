namespace ClientLibrary.Models.Authentication.Rol.Professional
{
    public class GetProfessional 
    {
        //public AppUser AppUser { get; set; } = default!;
        //public ICollection<ProfessionalLicense> Licenses { get; set; } = new List<ProfessionalLicense>();
        //public ICollection<ProfessionalGroup> ProfessionalGroups { get; set; } = new List<ProfessionalGroup>();
        //public ICollection<ProfessionalCategory> ProfessionalCategories { get; set; } = new List<ProfessionalCategory>();
        //public ICollection<ServiceOffering> ServiceOfferings { get; set; } = new List<ServiceOffering>();
        //public ICollection<Service> Services { get; set; } = new List<Service>();
        //public ICollection<RatingService> Ratings { get; set; } = new List<RatingService>();

        public string AppUserId { get; set; } = default!; // = Professional.Id
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Resúmenes para no cargar todo el grafo
        public List<string> Licenses { get; set; } = new();      // Números / códigos
        public List<string> Categories { get; set; } = new();    // Nombres de categorías
        public int ServicesCount { get; set; }
        public double AvgRating { get; set; }                    // promedio simple como ejemplo
    }
}
