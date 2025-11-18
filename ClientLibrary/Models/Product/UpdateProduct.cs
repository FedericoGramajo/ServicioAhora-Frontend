using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Models.Product
{
    public class UpdateProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        //ejemplo para commit
    }
}
