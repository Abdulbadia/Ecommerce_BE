using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.models
{
    public class Brand
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string BName { get; set; }
        public virtual List<Product>? Products { get; set; }

    }
}
