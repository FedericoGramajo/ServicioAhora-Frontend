using ClientLibrary.Models.Product;
using ClientLibrary.Models.ServicioAhora.ServOffering;

namespace ClientLibrary.Models.Category
{
    public class GetCategory : CategoryBase
    {
        public Guid Id { get; set; }

        //public ICollection<ProfessionalCategory>? ProfessionalCategories { get; set; }
        public ICollection<GetServiceOffering>? ServiceOfferingsCategories { get; set; }
    }
}
