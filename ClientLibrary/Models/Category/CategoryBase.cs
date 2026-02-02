using System.ComponentModel.DataAnnotations;

namespace ClientLibrary.Models.Category
{
    public class CategoryBase
    {
        [Required]
        public string? Name { get; set; }

        public string? Icon { get; set; }
        public bool IsFeatured { get; set; }
    }
}
