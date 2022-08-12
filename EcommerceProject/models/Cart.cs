using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceProject.models
{
    public class Cart
    {
        [Key]
        public int id { get; set; }
        public decimal totalPrice { get; set; }
        public decimal discountedTotal { get; set; }
        public int userId { get; set; }
        public int totalProducts { get; set; }
        public int totalQuantity { get; set; }
        public virtual ICollection<CartItems> Items { get; set; }
          = new HashSet<CartItems>();
    }
}
