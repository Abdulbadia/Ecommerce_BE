using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string CatName { get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}
