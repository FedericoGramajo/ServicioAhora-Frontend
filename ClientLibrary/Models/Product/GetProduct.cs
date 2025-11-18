using ClientLibrary.Models.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Models.Product
{
    public class GetProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        public GetCategory? Category{ get; set; }
        public DateTime CreatedDate { get; set; }
        public bool isNew => DateTime.Now <= CreatedDate.AddDays(7);
    }
}
