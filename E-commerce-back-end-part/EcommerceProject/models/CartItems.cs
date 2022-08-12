using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceProject.models
{
    public class CartItems
    {
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        [ForeignKey("product")]
        public int productID { get; set; }
        public Product product { get; set; }

        public int Quantity { get; set; }


    }
}
